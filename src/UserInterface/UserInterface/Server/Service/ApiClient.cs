using Common.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace UserInterface.Server.Service
{
    public class ApiClient
    {
        private readonly ApiSettings _apiSettings = new ApiSettings();
        public ApiClient()
        {
            _apiSettings.NeiWeb = "http://localhost:60004/";
            _apiSettings.NeiSys = "http://localhost:60001/";
            _apiSettings.Issuer = "neitek2024";
            _apiSettings.Key = "ramketG@24";
        }

        private class LoginToken
        {
            public string Token { get; set; }
        }

        private HttpClient GetClient(string service, string uri)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            switch (service)
            {
                case "tek":
                    client.BaseAddress = new Uri(_apiSettings.NeiSys + "nei/");
                    var tokenJSON = GetToken(client, service);
                    if (!string.IsNullOrWhiteSpace(tokenJSON) && !string.IsNullOrWhiteSpace(tokenJSON))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJSON);
                    break;
            }
            return client;
        }

        public string GetToken(HttpClient _client, string service)
        {
            string uri = string.Empty;
            if (service == "tek")
                uri = "api/login";
            if (!string.IsNullOrEmpty(uri) && !string.IsNullOrWhiteSpace(uri))
            {
                var response = _client.PostAsJsonAsync(uri, new ApiLoginVM() { UserName = _apiSettings.Issuer, Password = _apiSettings.Key }).Result;
                return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
            }
            else
                return string.Empty;
        }

        public Response<TResult> Get<TResult>(string service, string uri)
        {
            var result = new Response<TResult>();
            HttpResponseMessage response = null;
            try
            {
                var client = GetClient(service, uri);
                if (service == "bxc")
                    response = client.GetAsync(client.BaseAddress).Result;
                else
                    response = client.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;
                result.Data = JsonConvert.DeserializeObject<TResult>(json);
                result.Code = ResponseCode.Success;
            }
            catch (Exception e)
            {
                if (response != null && response.Content != null)
                    result.Message = response.Content.ReadAsStringAsync().Result;
                else
                    result.Message = e.ToString();
                result.Code = ResponseCode.Error;
            }
            return result;
        }

        public Response<TResponse> Post<TResponse, TRequest>(string service, string uri, TRequest model)
        {
            //Al response se le manda un tipo de dato del que sea, solo para que funcione como validacion de operacion exitosa y porder regresar la respuesta de la operacion
            var result = new Response<TResponse>();
            HttpResponseMessage response = null;
            try
            {
                var client = GetClient(service, uri);
                response = client.PostAsJsonAsync(uri, model ?? new object { }).Result;
                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;
                result.Data = JsonConvert.DeserializeObject<TResponse>(json);
                result.Code = ResponseCode.Success;
            }
            catch (Exception e)
            {
                var msg = string.Empty;
                if (response != null && response.Content != null)
                    msg = response.Content.ReadAsStringAsync().Result;
                result.Code = ResponseCode.Error;
                result.Message = string.IsNullOrEmpty(msg) ? e.ToString() : msg;
            }
            return result;
        }
    }
}