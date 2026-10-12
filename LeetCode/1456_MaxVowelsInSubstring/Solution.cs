public class Solution {
    public int MaxVowels(string s, int k) {
      int count =0;
      for(int i =0 ;i< k;i++ ){
        if(IsVowel(s[i]))
        {
            count++;
        }}
int maxVowels=count;

for(int i =k ;i<s.Length;i++)
{
    if (IsVowel(s[i - k]))
            {
                count--;
            }
    if(IsVowel(s[i]))    {
        count++;
    }   

    maxVowels = Math.Max(maxVowels,count) ;
    if (maxVowels == k)
            {
                return k;
            }
            

}
return maxVowels;
    }
private bool IsVowel(char c)
{
    return c == 'a' || c == 'e' || c == 'i'
        || c == 'o' || c == 'u';
}
      
}