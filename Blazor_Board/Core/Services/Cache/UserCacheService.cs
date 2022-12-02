using Blazor_Board.Models.Data;
using Blazored.LocalStorage;

namespace Blazor_Board.Core.Services.Cache
{
    public class UserCacheService : ICacheService<User>
    {
        private readonly string _userKey = "User";
        ILocalStorageService _storageService;

        public UserCacheService(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <summary>
        /// Uses the <see cref="_userKey"/> to get the data
        /// <para><see cref="_userKey"/> needs to be defined to use this method</para>
        /// </summary>
        /// <returns>A list of users</returns>
        public async Task<List<User>> GetAll()
        {
            return await _storageService.GetItemAsync<List<User>>(_userKey);
        }

        /// <summary>
        /// Calls <see cref="GetAll"/> to iterate through 
        /// 
        /// </summary>
        /// <param name="id">Is the user id</param>
        /// <returns>The user with a macthing Id or null if nothing is found</returns>
        public async Task<User?> Get(int id)
        {
            var users = await GetAll();

            if (users is null)
                return null;

            User? exist = users.FirstOrDefault(x => x.Id == id);

            if (exist is null)
                return null;

            return exist;
        }

        /// <summary>
        /// Uses the <see cref="GetAll"/> to get all the data stored
        /// <para>Then adds a user, if the user does not exist in storage</para>
        /// <para>And lastly it will call <see cref="UpdateAll(List{User})"/> to save the changed list to the storage</para>
        /// </summary>
        /// <param name="user">Is the user that is saved in storage</param>
        public async void Add(User user)
        {
            var users = await GetAll();
            bool exist = await Exist(user);
            
            if (exist)
                return;

            users.Add(user);

            UpdateAll(users);
        }

        /// <summary>
        /// If the storage is empty it will add the list to storage as bulk insert
        /// <para>If not it will use <see cref="Add(User)"/> to insert one by one</para>
        /// </summary>
        /// <param name="users">List of users to insert</param>
        public async void AddAll(List<User> users)
        {
            var allUsers = await GetAll();
            if (allUsers is null)
            {
                await _storageService.SetItemAsync(_userKey, users);
                return;
            }

            foreach (var user in users)
            {
                if (allUsers.Contains(user))
                    continue;
                
                Add(user);
            }
        }

        /// <summary>
        /// Uses the <see cref="UpdateAll(List{User})"/> to update the storage with the edited user
        /// 
        /// <para>Checks the list from <see cref="GetAll"/> for the user that should be updated returns if nothing is found</para>
        /// </summary>
        /// <param name="user">User to be updated</param>
        public async void Update(User user)
        {
            var users = await GetAll();
            if (users is null)
                return;

            var exist = users.FirstOrDefault(x => x.Id == user.Id);
            if (exist is null)
                return;

            users.Remove(exist);
            users.Add(user);
            UpdateAll(users);
        }

        /// <summary>
        /// It will only use the bulk feature if storage is null
        /// <para>If not it will use <see cref="Update(User)"/> in a foreach</para>
        /// </summary>
        /// <param name="users">Users to update</param>
        public async void UpdateAll(List<User> users)
        {
            var allUsers = await GetAll();
            if (allUsers is null)
                await _storageService.SetItemAsync(_userKey, users);

            foreach (var user in users)
            {
                Update(user);
            }
        }

        /// <summary>
        /// Uses the <see cref="GetAll"/> to get all the data from storage
        /// <para>If the storage is null it will return</para>
        /// <para>Iterates through the data to find a match on user id</para>
        /// <para>When it finds the section it will remove it from the data list and the use <see cref="UpdateAll(List{Section})"/> to save the changed list</para>
        /// </summary>
        /// <param name="user">Section to delete</param>
        public async void Remove(User user)
        {
            var users = await GetAll();
            if (users is null)
                return;

            var exist = users.FirstOrDefault(x => x.Id == user.Id);
            if (exist is null)
                return;

            users.Remove(exist);
            UpdateAll(users);
        }

        /// <summary>
        /// Gets all user data to find out if the section exist in the storage
        /// 
        /// </summary>
        /// <param name="user">User to look for</param>
        /// <returns>True if exist</returns>
        public async Task<bool> Exist(User user)
        {
            var users = await GetAll();
            return users.Exists(x => x.Id == user.Id);
        }

        /// <summary>
        /// Used to clear the whole storage 
        /// <para>Be careful to use this because it will delete all data not only user data</para>
        /// </summary>
        public async void Clear()
        {
            await _storageService.ClearAsync();
        }
    }
}
