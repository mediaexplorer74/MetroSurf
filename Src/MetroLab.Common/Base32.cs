// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Base32
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System.Text;

#nullable disable
namespace MetroLab.Common
{
  public static class Base32
  {
    private static string base32Chars = "ybndrfg8ejkmcpqxot1uwisza345h769";

    public static string Base32Encode(byte[] bytes)
    {
      int index1 = 0;
      int num1 = 0;
      StringBuilder stringBuilder = new StringBuilder((bytes.Length + 7) * 8 / 5);
      while (index1 < bytes.Length)
      {
        int num2 = bytes[index1] >= (byte) 0 ? (int) bytes[index1] : (int) bytes[index1] + 256;
        int index2;
        if (num1 > 3)
        {
          int num3 = index1 + 1 >= bytes.Length ? 0 : (bytes[index1 + 1] >= (byte) 0 ? (int) bytes[index1 + 1] : (int) bytes[index1 + 1] + 256);
          int num4 = num2 & (int) byte.MaxValue >> num1;
          num1 = (num1 + 5) % 8;
          index2 = num4 << num1 | num3 >> 8 - num1;
          ++index1;
        }
        else
        {
          index2 = num2 >> 8 - (num1 + 5) & 31;
          num1 = (num1 + 5) % 8;
          if (num1 == 0)
            ++index1;
        }
        stringBuilder.Append(Base32.base32Chars[index2]);
      }
      return stringBuilder.ToString();
    }

    public static byte[] Base32Decode(string base32)
    {
      byte[] numArray = new byte[base32.Length * 5 / 8];
      int index1 = 0;
      int num1 = 0;
      int index2 = 0;
      for (; index1 < base32.Length; ++index1)
      {
        int num2 = Base32.base32Chars.IndexOf(base32[index1]);
        if (num1 <= 3)
        {
          num1 = (num1 + 5) % 8;
          if (num1 == 0)
          {
            numArray[index2] |= (byte) num2;
            ++index2;
            if (index2 >= numArray.Length)
              break;
          }
          else
            numArray[index2] |= (byte) (num2 << 8 - num1);
        }
        else
        {
          num1 = (num1 + 5) % 8;
          numArray[index2] |= (byte) (num2 >> num1);
          ++index2;
          if (index2 < numArray.Length)
            numArray[index2] |= (byte) (num2 << 8 - num1);
          else
            break;
        }
      }
      return numArray;
    }
  }
}
