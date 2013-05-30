using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


	public class Luhn
	{
        public static bool ValidateCCNum(string CCnum, string CreditCardType)
        {
            CCnum = CCnum.Trim().Replace(" ", "").Replace("-", "");
            bool IsValid = false;
            switch (CreditCardType.ToLower().Replace(" ",""))
            {
                case "mastercard":
                    if (CCnum.Length == 16 
                        && CCnum.Substring(0, 1) == "5" 
                        && int.Parse(CCnum.Substring(1, 1)) >= 1
                        && int.Parse(CCnum.Substring(1, 1)) <= 5)
                    {
                        IsValid = true;
                    }
                    break;
                case "visa":
                    if ((CCnum.Length == 13 
                        || CCnum.Length == 16)
                        && CCnum.Substring(0, 1) == "4")
                    {
                        IsValid = true;
                    }
                    break;
                case "americanexpress":
                    if (CCnum.Length == 15
                        && (CCnum.Substring(0, 2) == "34"
                        || CCnum.Substring(0, 2) == "37"))
                    {
                        IsValid = true;
                    }
                    break;
            }
            return IsValid ? ValidateCCNum(CCnum) : false;
        }
		public static bool ValidateCCNum(string CCnum)
		{
			if (String.IsNullOrEmpty(CCnum))
				return false;

			int total = 0;

			string cleanNumber = CCnum.Replace("-", "");
			if (!IsNumeric(cleanNumber))
				return false;

			for (int i = cleanNumber.Length - 1; i >= 0; i -= 2)
			{
				total += Int16.Parse(cleanNumber.Substring(i, 1));
				if (i > 0)
					total += LuhnSum(cleanNumber.Substring(i - 1, 1));
			}

			return (total % 10 == 0);
		}

		public static int LuhnSum(string number)
		{
			int x = Int16.Parse(number) * 2;
			if (x < 10)
				return x;

			return (x % 10) + 1;
		}

		public static bool IsNumeric(string number)
		{
			bool retVal = true;
			short numOut;

			try
			{
				for (int x = 0; x < number.Length; x++)
				{
					if (!Int16.TryParse(number.Substring(x, 1), out numOut))
						retVal = false;
				}
			}
			catch { }

			return retVal;
		}

	}

