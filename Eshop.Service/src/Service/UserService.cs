using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepoAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Service.src.Validation;

namespace Eshop.Service.src.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserReadDTO> CreateUserAsync(UserCreateDTO userDTO)
        {
            UserValidation.ValidateUserCreateDTO(userDTO);

            var user = _mapper.Map<User>(userDTO);

            var isUserExistWithEmail = await _userRepo.UserExistsByEmailAsync(user.Email);
            
            if (isUserExistWithEmail )
            {
                throw new ArgumentException($"A user with the email {user.Email} already exists.");
            }

            var createdUser = await _userRepo.CreateAsync(user);

            return _mapper.Map<UserReadDTO>(createdUser);
        }

        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
           
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return await _userRepo.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllUsersAsync(QueryOptions options)
        {
            var users = await _userRepo.GetAllUsersAsync(options);

            return _mapper.Map<IEnumerable<UserReadDTO>>(users);
        }

        public async Task<UserReadDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return _mapper.Map<UserReadDTO>(user);
        }

        public async Task<bool> UpdateUserByIdAsync(Guid id, UserUpdateDTO userDTO)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            UserValidation.ValidateUserUpdateDTO(userDTO);

            _mapper.Map(userDTO, existingUser);

            return await _userRepo.UpdateAsync(existingUser);
        }
    }
}
