using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace TicketHelper
{
    public static class SundayAPI
    {
        [DllImport("Sunday.dll", CharSet = CharSet.Ansi)]
        public static extern bool GetCodeFromBuffer(Int32 LibFileIndex, Byte[] FileBuffer, Int32 ImgBufLen, StringBuilder Code);
        [DllImport("Sunday.dll", CharSet = CharSet.Ansi)]
        public static extern bool LoadLibFromBuffer(Byte[] FileBuffer, Int32 BufLen, String Password);
    }
}
