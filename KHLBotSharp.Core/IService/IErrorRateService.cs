
namespace KHLBotSharp.IService
{
    public interface IErrorRateService
    {
        void AddError();
        void ReportStatus();
        void CheckResetError();
    }
}
