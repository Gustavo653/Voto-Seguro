using VotoSeguro.Domain.Enum;
using VotoSeguro.Domain.Identity;
using VotoSeguro.DTO;
using VotoSeguro.DTO.Base;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Infrastructure.Service;
using VotoSeguro.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VotoSeguro.Service
{
    public class AccountService(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IUserRepository userRepository,
                                ITokenService tokenService,
                                IHttpContextAccessor httpContextAccessor) : IAccountService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext!.Session;

        private async Task<SignInResult> CheckUserPassword(User user, UserLoginDTO userLoginDTO)
        {
            try
            {
                return await _signInManager.CheckPasswordSignInAsync(user, userLoginDTO.Password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar senha do usuário. Erro: {ex.Message}");
            }
        }

        private async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                return await _userRepository.GetEntities()
                    .Include(x => x.Tenant)
                    .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                    .FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o usuário. Erro: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> Login(UserLoginDTO userDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var user = await GetUserByEmail(userDTO.Email);

                if (user == null)
                {
                    responseDTO.Code = 401;
                    responseDTO.Message = "Não autenticado! Verifique o email e a senha inserida!";
                    Log.Warning(responseDTO.Message);
                    return responseDTO;
                }

                var password = await CheckUserPassword(user, userDTO);
                if (!password.Succeeded)
                {
                    responseDTO.Code = 401;
                    responseDTO.Message = "Não autenticado! Verifique o email e a senha inserida!";
                    Log.Warning(responseDTO.Message);
                    return responseDTO;
                }

                responseDTO.Object = new
                {
                    userName = user.UserName,
                    role = user.UserRoles.FirstOrDefault()?.Role.Name,
                    name = user.Name,
                    email = user.Email,
                    token = await _tokenService.CreateToken(user)
                };
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> GetCurrent()
        {
            ResponseDTO responseDTO = new();
            try
            {
                var email = _session.GetString(Consts.ClaimEmail)!;
                Log.Information("Obtendo o usuário atual: {email}", email);
                responseDTO.Object = await GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> CreateUser(UserDTO userDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                Log.Information("Role do usuário do DTO: {role}", userDTO.Role);
                if (userDTO.Role == RoleName.Admin)
                {
                    var requestUser = await _userManager.FindByIdAsync(_session.GetString(Consts.ClaimUserId)!);
                    var requestUserRoleAdmin = await _userManager.IsInRoleAsync(requestUser!, RoleName.Admin.ToString());
                    if (!requestUserRoleAdmin)
                    {
                        Log.Warning("O usuário {user} não pertence ao role {role}", requestUser!.Id, RoleName.Admin);
                        responseDTO.Code = 403;
                        return responseDTO;
                    }
                    userDTO.IdTenant = null;
                    userDTO.Coren = null;
                }
                else
                {
                    userDTO.IdTenant = Convert.ToInt32(string.IsNullOrEmpty(_session.GetString(Consts.ClaimTenantId)) ? userDTO.IdTenant.ToString() : _session.GetString(Consts.ClaimTenantId));
                    Log.Information("Atribuindo ao usuário novo o tenant: {idTenant}", userDTO.IdTenant);
                }

                var user = await _userManager.FindByEmailAsync(userDTO.Email);
                if (user != null)
                {
                    responseDTO.SetBadInput($"Já existe um usuário cadastrado com este e-mail: {userDTO.Email}!");
                    return responseDTO;
                }

                if (userDTO.Password == null)
                {
                    responseDTO.SetBadInput($"A senha deve ser preenchida");
                    return responseDTO;
                }

                var userEntity = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    NormalizedEmail = userDTO.Email.ToUpper(),
                    NormalizedUserName = userDTO.Email.ToUpper(),
                    TenantId = userDTO.IdTenant
                };

                userEntity.PasswordHash = _userManager.PasswordHasher.HashPassword(userEntity, userDTO.Password);

                await _userRepository.InsertAsync(userEntity);
                await _userRepository.SaveChangesAsync();
                await _userManager.UpdateSecurityStampAsync(userEntity);

                Log.Information("Usuário persistido id: {id}", userEntity.Id);

                await AddUserInRole(userEntity, userDTO.Role);
                await _userRepository.SaveChangesAsync();

                Log.Information("Usuário adicionado no role: {role}", userDTO.Role);
                responseDTO.Object = userEntity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateUser(int id, UserDTO userDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                Log.Information("Role do usuário do DTO: {role}", userDTO.Role);
                if (userDTO.Role == RoleName.Admin)
                {
                    var requestUser = await _userManager.FindByIdAsync(_session.GetString(Consts.ClaimUserId)!);
                    var requestUserRoleAdmin = await _userManager.IsInRoleAsync(requestUser!, RoleName.Admin.ToString());
                    if (!requestUserRoleAdmin)
                    {
                        Log.Warning("O usuário {user} não pertence ao role {role}", requestUser!.Id, RoleName.Admin);
                        responseDTO.Code = 403;
                        return responseDTO;
                    }
                    userDTO.IdTenant = null;
                    userDTO.Coren = null;
                }
                else
                {
                    userDTO.IdTenant = Convert.ToInt32(string.IsNullOrEmpty(_session.GetString(Consts.ClaimTenantId)) ? userDTO.IdTenant.ToString() : _session.GetString(Consts.ClaimTenantId));
                    Log.Information("Atribuindo ao usuário novo o tenant: {idTenant}", userDTO.IdTenant);
                }

                var userEntity = await _userRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == id);
                if (userEntity == null)
                {
                    responseDTO.SetBadInput($"Usuário não encotrado com este id: {id}!");
                    return responseDTO;
                }

                userEntity.Name = userDTO.Name;

                if (userDTO.Password != null)
                    userEntity.PasswordHash = _userManager.PasswordHasher.HashPassword(userEntity, userDTO.Password);

                await _userRepository.SaveChangesAsync();

                Log.Information("Usuário persistido id: {id}", userEntity.Id);

                var userRoles = await _userManager.GetRolesAsync(userEntity);
                await _userManager.RemoveFromRolesAsync(userEntity, userRoles);
                await _userRepository.SaveChangesAsync();
                await AddUserInRole(userEntity, userDTO.Role);
                Log.Information("Usuário adicionado no role: {role}", userDTO.Role);

                await _userRepository.SaveChangesAsync();

                responseDTO.Object = userEntity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> RemoveUser(int id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var session = _session.GetString(Consts.ClaimTenantId);
                var userEntity = await _userRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == id && x.TenantId == (session == null ? x.TenantId : Convert.ToInt32(session)));
                if (userEntity == null)
                {
                    responseDTO.SetBadInput($"Usuário não encontrado com este id: {id}!");
                    return responseDTO;
                }

                var userRoles = await _userManager.GetRolesAsync(userEntity);
                await _userManager.RemoveFromRolesAsync(userEntity, userRoles);
                await _userManager.DeleteAsync(userEntity);

                Log.Information("Usuário removido id: {id}", userEntity.Id);

                responseDTO.Object = userEntity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        private async Task AddUserInRole(User user, RoleName role)
        {
            if (!await _userManager.IsInRoleAsync(user, role.ToString()))
                await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public async Task<ResponseDTO> GetUsers()
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await _userRepository.GetEntities()
                                                          .Where(x => _session.GetString(Consts.ClaimTenantId) == string.Empty ||
                                                                      _session.GetString(Consts.ClaimTenantId) == x.TenantId.ToString())
                                                          .Select(x => new
                                                          {
                                                              x.Id,
                                                              x.Name,
                                                              x.Email,
                                                              x.UserName,
                                                              roles = string.Join(",", x.UserRoles.Select(ur => ur.Role.NormalizedName))
                                                          }).ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }
    }
}
