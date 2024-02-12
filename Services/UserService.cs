using BlogApi.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace BlogApi.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private readonly ILogger<UserService> _logger;

        public UserService(IMongoDatabase database, ILogger<UserService> logger)
        {
            _usersCollection = database.GetCollection<User>("User");
            _passwordHasher = new PasswordHasher<User>();
            _logger = logger; 
        }

        public async Task<User> RegisterUser(UserRegisterDto userRegisterDto)
        {
            try
            {
                // Primero, verifica si ya existe un usuario con el mismo correo electrónico
                var existingUser = await _usersCollection.Find(u => u.Email == userRegisterDto.Email).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    throw new ApplicationException("El correo electrónico ya está registrado.");
                }

                // Lista de correos electrónicos permitidos para el registro
                var allowedEmails = new List<string>
                {
                    "colaborador1@ejemplo.com",
                    "colaborador2@ejemplo.com",
                };

                // Verifica si el correo electrónico proporcionado está en la lista de permitidos
                if (!allowedEmails.Contains(userRegisterDto.Email))
                {
                    throw new ApplicationException("El correo electrónico proporcionado no está autorizado para el registro.");
                }

                var user = new User
                {
                    Username = userRegisterDto.Username,
                    PasswordHash = _passwordHasher.HashPassword(null, userRegisterDto.Password),
                    Email = userRegisterDto.Email,
                    FullName = userRegisterDto.FullName
                };

                await _usersCollection.InsertOneAsync(user);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar un nuevo usuario.");

                throw new ApplicationException("Ocurrió un error al registrar el usuario", ex);
            }
        }

        public async Task<User?> ValidateUserCredentials(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _usersCollection.Find(u => u.Email == userLoginDto.Email).FirstOrDefaultAsync();

                if (user == null)
                {
                    return null;
                }

                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las credenciales de usuario.");

                throw new ApplicationException("Error al validar las credenciales de usuario.", ex);
            }
        }
    }
}
