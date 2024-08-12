using AppCore.Infrastructure.Grpc.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Grpc.Common
{
    public class GrpcClientUrlSettings
    {
        public string? AlertService { get; set; }
        public string? CoreService { get; set; }
        public string? IdentityService { get; set; }
        public string? NotificationService { get; set; }
        public string? ReportService { get; set; }
        public string? CommonDataService { get; set; }
        public string? GetUrlByServiceNameEnum(ServiceNameEnum serviceNameEnum)
        {
            switch (serviceNameEnum)
            {
                case ServiceNameEnum.CoreService:
                    return CoreService;
                case ServiceNameEnum.IdentityService:
                    return IdentityService;
                default:
                    return "";
            }
        }
    }
}
