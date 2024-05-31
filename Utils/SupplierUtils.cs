using ProductRegistryAPI.Models;
using System.Text.RegularExpressions;

namespace ProductRegistryAPI.Utils
{
    public static class SupplierUtils
    {
        public static bool IsValidCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj)) return false;

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14) return false;
            string tempCnpj = Regex.Replace(cnpj, "[^0-9]", "");

            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            int rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            string digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            return cnpj.EndsWith(digit);
        }

        public static string FormatCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj)) return null;

            var newCnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (newCnpj.Length != 14) return null;

            return Convert.ToUInt64(newCnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        public static async Task<Address> GetAddressFromCep(string cep)
        {
            using (var client = new HttpClient())
            {
                string numericCep = Regex.Replace(cep, "[^0-9]", "");
                var response = await client.GetAsync($"https://viacep.com.br/ws/{numericCep}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Address>(json);
                }
                return null;
            }
        }
    }
}
