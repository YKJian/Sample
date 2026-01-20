namespace Magic.Buffs.Extensions
{
    public static class BuffExtensions 
    {
        public static void Refresh(this IBuff buff, BuffContainer buffContainer)
        {
            buff.Deinitialize();
            buff.Initialize(buffContainer);
        }
    }
}