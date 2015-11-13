using System;
namespace Hk.Infrastructures.Localization
{
   public class LocaleResource
    {
       public static string GetMessageContent(string codeKey)
       {
           return Configs.Config.GetMessageContent(codeKey);
       }
    }
}
