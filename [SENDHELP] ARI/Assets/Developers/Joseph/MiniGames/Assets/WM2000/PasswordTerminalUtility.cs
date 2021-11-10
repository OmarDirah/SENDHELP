public static class StringExtension
{
    public static string Anagram(this string str)
    {
        string attempt = Shuffle(str);
        while (attempt == str)
        {
            attempt = Shuffle(str);
        }
        return attempt;
    }

    private static string Shuffle(string str)
    {
        char[] characters = str.ToCharArray();
        System.Random randomRange = new System.Random();
        int numberOfCharacters = characters.Length;
        while (numberOfCharacters > 1)
        {
            numberOfCharacters--;
            int index = randomRange.Next(numberOfCharacters + 1);
            var value = characters[index];
            characters[index] = characters[numberOfCharacters];
            characters[numberOfCharacters] = value;
        }
        return new string(characters);
    }
}