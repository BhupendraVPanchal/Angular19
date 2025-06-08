using System.Security.Cryptography;
using System.Text;

namespace Helper_CL.custom
{
    public class _enc_dec
    {
        //private string getPassword()
        //{
        //    return getPasskey("123456");
        //}
        //private string getApplicationIdPassword()
        //{
        //    return getPasskey("654321");
        //}

        //private string getPasskey(string PassKeyString)
        //{
        //    return Encoding.ASCII.GetString(this.GetHashMD5(PassKeyString));
        //}

        //private byte[] GetHashMD5(string StringToHash)
        //{
        //    StringToHash = StringToHash.Replace(" ", "");
        //    byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(StringToHash));
        //    byte[] numArray = new byte[24];
        //    int index1 = 0;
        //    for (int index2 = 0; index2 <= numArray.Length - 1; ++index2)
        //    {
        //        numArray[index2] = hash[index1];
        //        ++index1;
        //        if (index1 >= 16)
        //            index1 = 0;
        //    }
        //    return numArray;
        //}

        //private byte[] GetHashSHA(string StringToHash)
        //{
        //    StringToHash = StringToHash.Replace(" ", "");
        //    return SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(StringToHash));
        //}

        //public string Encrypt(string TextToEncrypt, bool ForApplicationId = false)
        //{
        //    string str = "";
        //    Aes aes = Aes.Create();
        //    aes.Key = this.GetHashSHA(ForApplicationId ? getApplicationIdPassword() : getPassword());
        //    aes.IV = new byte[aes.BlockSize / 8];
        //    byte[] bytes = Encoding.UTF8.GetBytes(TextToEncrypt);
        //    str = Convert.ToBase64String(aes.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
        //    return str;
        //}

        //public string Decrypt(string Text2Decrypt, bool ForApplicationId = false)
        //{
        //    string str = "";
        //    Aes aes = Aes.Create();
        //    aes.Key = this.GetHashSHA(ForApplicationId ? getApplicationIdPassword() : getPassword());
        //    aes.IV = new byte[aes.BlockSize / 8];
        //    byte[] inputBuffer = Convert.FromBase64String(Text2Decrypt);
        //    str = Encoding.UTF8.GetString(aes.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
        //    return str;
        //}

        string strEncrypted, strDecrypted;
        TripleDESCryptoServiceProvider des;
        MD5CryptoServiceProvider hashmd5;
        byte[] pwdhash, buff;

        //create a secret password. the password is used to encrypt
        //and decrypt strings. Without the password, the encrypted
        //string cannot be decrypted and is just garbage. You must
        //use the same password to decrypt an encrypted string as the
        //string was originally encrypted with.

        //////public Encrypto(string strPassword, string TextToEncryptOrDecrypt)
        //////{
        //////  //generate an MD5 hash from the password. 
        //////  //a hash is a one way encryption meaning once you generate
        //////  //the hash, you cant derive the password back from it.
        //////  hashmd5 = new MD5CryptoServiceProvider();
        //////  pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strPassword));
        //////  hashmd5 = null;

        //////  //implement DES3 encryption
        //////  des = new TripleDESCryptoServiceProvider();

        //////  //the key is the secret password hash.
        //////  des.Key = pwdhash;

        //////  //the mode is the block cipher mode which is basically the
        //////  //details of how the encryption will work. There are several
        //////  //kinds of ciphers available in DES3 and they all have benefits
        //////  //and drawbacks. Here the Electronic Codebook cipher is used
        //////  //which means that a given bit of text is always encrypted
        //////  //exactly the same when the same password is used.
        //////  des.Mode = CipherMode.ECB; //CBC, CFB


        //////  //----- encrypt an un-encrypted string ------------
        //////  //the original string, which needs encrypted, must be in byte
        //////  //array form to work with the des3 class. everything will because
        //////  //most encryption works at the byte level so you'll find that
        //////  //the class takes in byte arrays and returns byte arrays and
        //////  //you'll be converting those arrays to strings.
        //////  buff = ASCIIEncoding.ASCII.GetBytes(TextToEncryptOrDecrypt);
        //////}

        ~_enc_dec()
        {
            //cleanup
            des = null;
        }

        public string GetEncryptedText(string strPassword, string TextToEncryptOrDecrypt)
        {
            //This will convert each byte into ASCII value
            TextToEncryptOrDecrypt = StringToChar(TextToEncryptOrDecrypt);
            //This will reverse the entire text
            TextToEncryptOrDecrypt = ReverseString(TextToEncryptOrDecrypt);

            //generate an MD5 hash from the password. 
            //a hash is a one way encryption meaning once you generate
            //the hash, you cant derive the password back from it.
            hashmd5 = new MD5CryptoServiceProvider();
            pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strPassword));
            hashmd5 = null;

            //implement DES3 encryption
            des = new TripleDESCryptoServiceProvider();

            //the key is the secret password hash.
            des.Key = pwdhash;

            //the mode is the block cipher mode which is basically the details of how the encryption will work. 
            //There are several kinds of ciphers available in DES3 and they all have benefits
            //and drawbacks. Here the Electronic Codebook cipher is used which means that a given bit of text 
            //is always encrypted exactly the same when the same password is used.
            des.Mode = CipherMode.ECB; //CBC, CFB

            //----- encrypt an un-encrypted string ------------
            //the original string, which needs encrypted, must be in byte array form to work with the des3 class. 
            //Everything will because most encryption works at the byte level so you'll find that
            //the class takes in byte arrays and returns byte arrays and you'll be converting those arrays to strings.
            buff = ASCIIEncoding.ASCII.GetBytes(TextToEncryptOrDecrypt);

            //encrypt the byte buffer representation of the original string and base64 encode the encrypted string. the reason the encrypted
            //bytes are being base64 encoded as a string is the encryption will have created some weird characters in there. Base64 encoding
            //provides a platform independent view of the encrypted string and can be sent as a plain text string to wherever.
            strEncrypted = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buff, 0, buff.Length));
            return strEncrypted;
        }

        public string GetDecryptedText(string strPassword, string TextToEncryptOrDecrypt)
        {

            //generate an MD5 hash from the password. 
            //a hash is a one way encryption meaning once you generate
            //the hash, you cant derive the password back from it.
            hashmd5 = new MD5CryptoServiceProvider();
            pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strPassword));
            hashmd5 = null;

            //implement DES3 encryption
            des = new TripleDESCryptoServiceProvider();

            //the key is the secret password hash.
            des.Key = pwdhash;

            //the mode is the block cipher mode which is basically the details of how the encryption will work. 
            //There are several kinds of ciphers available in DES3 and they all have benefits
            //and drawbacks. Here the Electronic Codebook cipher is used which means that a given bit of text 
            //is always encrypted exactly the same when the same password is used.
            des.Mode = CipherMode.ECB; //CBC, CFB

            ////----- encrypt an un-encrypted string ------------
            ////the original string, which needs encrypted, must be in byte array form to work with the des3 class. 
            ////Everything will because most encryption works at the byte level so you'll find that
            ////the class takes in byte arrays and returns byte arrays and you'll be converting those arrays to strings.
            //buff = ASCIIEncoding.ASCII.GetBytes(TextToEncryptOrDecrypt);

            //----- decrypt an encrypted string ------------
            //whenever you decrypt a string, you must do everything you did to encrypt the string, but in reverse order. 
            //To encrypt, first a normal string was des3 encrypted into a byte array and then base64 encoded for reliable
            //transmission. So, to decrypt this string, first the base64 encoded string must be 
            //decoded so that just the encrypted byte array remains.
            buff = Convert.FromBase64String(TextToEncryptOrDecrypt);

            //decrypt DES 3 encrypted byte buffer and return ASCII string
            strDecrypted = ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buff, 0, buff.Length));

            //This will reverse the entire text, as while encrypting the text we already have revsersed the original text
            strDecrypted = ReverseString(strDecrypted);

            //This will convert ascii char values to string as we have already converted string to ascii while encrypting
            strDecrypted = CharToString(strDecrypted);

            return strDecrypted;
        }

        public string ReverseString(string String2Reverse)
        {
            string strRtnValue = "";
            for (int i = String2Reverse.Length; i > 0; i--)
            {
                strRtnValue += String2Reverse.Substring(i - 1, 1);
            }
            return strRtnValue;
        }

        public string CharToString(string Chars2String)
        {
            string strRtnValue = "";
            string strCharValue = "";
            int intCharValue = 0;
            for (int i = 0; i <= Chars2String.Length - 1; i += 3)
            {
                intCharValue = Convert.ToInt32(Chars2String.Substring(i, 3).Trim());
                strCharValue = Convert.ToChar(intCharValue).ToString();
                strRtnValue += strCharValue;
            }
            return strRtnValue;
        }

        public string StringToChar(string String2Char)
        {
            string strRtnValue = "";
            string strCharValue = "";
            for (int i = 0; i <= String2Char.Length - 1; i++)
            {

                strCharValue = Convert.ToInt32(Convert.ToChar(String2Char.Substring(i, 1))).ToString();
                if (strCharValue.Trim().Length == 2) //Adding a leading ZERO
                {
                    strCharValue = "0" + strCharValue;
                }
                strRtnValue += strCharValue;
            }
            return strRtnValue;
        }
    }
}
