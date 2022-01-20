using UnityEngine;
using System;
namespace TMPro
{
    /// <summary>
    /// EXample of a Custom Character Input Validator to only allow digits from 0 to 9.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "Validator", menuName = "TextMeshPro/Input Validators/Digits")]

    public class TMP_DigitValidator : TMP_InputValidator
    {
        // Custom text input validation function
        public override char Validate(ref string text, ref int pos, char ch)
        {
           
            //Allow handling . or , for float input
            bool ok = false;
            if (char.IsNumber(ch))
            {
                text = text.Insert(pos, ch.ToString());
                ok = true;
            }
            else
            {
                if ((ch == '.') || (ch == ','))
                {
                    ok = (!text.Contains(".")) && (!text.Contains(","));
                    if (ok)
                    {
                        text = text.Insert(pos, ch.ToString());
                    }
                }
            }

            if (ok)
            {
                pos++;
                return (ch);
            }
            else
            {
                return '\0';
            }
        }
    }
}