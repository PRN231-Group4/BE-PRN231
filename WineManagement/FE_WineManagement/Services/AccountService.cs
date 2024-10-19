using BusinessLayer.Modal.Request.Account;
using DataLayer.Models;
using System.Security.Principal;

namespace FE_WineManagement.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            var response = await _httpClient.GetAsync("/odata/account/getallaccount");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Account>>();
            }

            return null;
        }
        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            var response = await _httpClient.GetAsync($"/odata/account/getaccountbyid/{accountId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Account>();
            }

            return null;
        }
        public async Task<bool> UpdateAccountAsync(int accountId, UpdateAccountDto request)
        {
            var content = JsonContent.Create(request);
            var response = await _httpClient.PutAsync($"/odata/account/updateaccount?accountId={accountId}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            var response = await _httpClient.DeleteAsync($"/odata/account/deleteuser/{accountId}");
            return response.IsSuccessStatusCode;
        }
    }
}
