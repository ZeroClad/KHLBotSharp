
namespace KHLBotSharp.IService
{
    /// <summary>
    /// 错误注册，达到一定错误数量会自动重启机器人
    /// </summary>
    public interface IErrorRateService
    {
        /// <summary>
        /// 添加错误
        /// </summary>
        void AddError();
        /// <summary>
        /// 写入log
        /// </summary>
        void ReportStatus();
        /// <summary>
        /// 类似心跳，检测如果错误短时间内出现太多自动重启，否则时间足够长则重置错误，继续运行Bot
        /// </summary>
        void CheckResetError();
    }
}
