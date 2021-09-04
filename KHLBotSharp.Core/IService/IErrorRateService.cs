
namespace KHLBotSharp.IService
{
    /// <summary>
    /// 错误注册，达到一定错误数量会自动重启机器人
    /// </summary>
    public interface IErrorRateService
    {
        void AddError();
        void ReportStatus();
        void CheckResetError();
    }
}
