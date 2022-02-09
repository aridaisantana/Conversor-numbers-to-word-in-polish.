using System;
namespace ConvertNumberIntoWords
{
    public interface IStringIterator
    {
        string Next();
        bool HasNext();
        int getConjugationType();
    }
}
