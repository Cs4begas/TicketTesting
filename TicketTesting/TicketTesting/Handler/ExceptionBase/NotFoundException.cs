namespace Hedwig.Handler
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base("000001", "ไม่พบข้อมูล")
        {
        }

        public NotFoundException(string errorMessage) : base("000001", errorMessage)
        {
        }
    }
}
