using Application.Service.SeedWork;
using Common.Model;

namespace Application.Service.Queries
{
    public sealed class GetLoginApi : IQuery<ApiLoginVM>
    {
        public ApiLoginVM DatosSesion { get; set; }

        public GetLoginApi(ApiLoginVM datosSesion)
        {
            DatosSesion = datosSesion;
        }
    }
}