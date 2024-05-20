using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepoAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Service.src.Validation;

namespace Eshop.Service.src.Service
{
    public class UserService : BaseService<User, UserCreateDTO, UserUpdateDTO, UserReadDTO>, IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordService _passwordService;

        public UserService(IUserRepository userRepo, IMapper mapper, IPasswordService passwordService)
            : base(userRepo, mapper)
        {
            _userRepo = userRepo;
            _passwordService = passwordService;
        }

        public override async Task<UserReadDTO> CreateAsync(UserCreateDTO userDTO)
        {
            UserValidation.ValidateUserCreateDTO(userDTO);

            var isUserExistWithEmail = await _userRepo.UserExistsByEmailAsync(userDTO.Email);

            if (isUserExistWithEmail)
            {
                throw AppException.Conflict($"A user with the email {userDTO.Email} already exists.");
            }

            var user = _mapper.Map<User>(userDTO);
            user.Password = _passwordService.HashPassword(user.Password, out var salt);
            user.Salt = salt;

            var createdUser = await _userRepo.CreateAsync(user);
            return _mapper.Map<UserReadDTO>(createdUser);
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllUsersAsync(QueryOptions options)
        {
            var users = await _userRepo.GetAllUsersAsync(options);
            return _mapper.Map<IEnumerable<UserReadDTO>>(users);
        }

        public override async Task<bool> UpdateAsync(Guid id, UserUpdateDTO userDTO)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw AppException.NotFound($"User with ID {id} not found.");
            }

            _mapper.Map(userDTO, existingUser);

            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                existingUser.Password = _passwordService.HashPassword(userDTO.Password, out var salt);
                existingUser.Salt = salt;
            }
            return await _userRepo.UpdateAsync(existingUser);
        }
    }
}
