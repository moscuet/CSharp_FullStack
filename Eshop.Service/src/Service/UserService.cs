using AutoMapper;
using System.Net;
using System.Text.Json;
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
        private readonly IPasswordService _passwordService;

        public UserService(IUserRepository userRepo, IMapper mapper, IPasswordService passwordService)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<UserReadDTO> CreateUserAsync(UserCreateDTO userDTO)
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

          public async Task<UserReadDTO> GetUserProfileAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw AppException.NotFound($"User with ID {id} not found.");
            }
            return _mapper.Map<UserReadDTO>(user);
        }
        
        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw AppException.NotFound($"User with ID {id} not found.");
            }

            return await _userRepo.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllUsersAsync(QueryOptions options)
        {
            var users = await _userRepo.GetAllUsersAsync(options);
            return _mapper.Map<IEnumerable<UserReadDTO>>(users);
        }

        public async Task<bool> UpdateUserByIdAsync(Guid id, UserUpdateDTO userDTO)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw AppException.NotFound($"User with ID {id} not found.");
            }

            Console.WriteLine($"Existing User: {JsonSerializer.Serialize(existingUser)}");

            _mapper.Map(userDTO, existingUser);

            Console.WriteLine($"Updated User from service: {JsonSerializer.Serialize(existingUser)}");

            return await _userRepo.UpdateAsync(existingUser);
        }
    }
}
