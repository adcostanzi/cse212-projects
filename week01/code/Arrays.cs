public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Initialize the array that will store the multiples, size will be the length input by the user
        var result = new double[length];
        // Start by creating a loop where the counter starts as 1 and adds 1 each interation until the counter is bigger than length
        for (int i = 1; i < length+1; i++)
        {
            // Mutiply the number by the counter and save the multiple in the array
            double calculation = number * i;
            result[i-1] = calculation;
        }

        return result; // return result
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Create a loop by the amount input
        for (int i = 0; i < amount; i++)
        {
            // Save the last value of the array into a variable, which will be inserted at the beginning of the list
            int toInsert = data[data.Count() - 1];
            // Remove the last value of the array
            data.RemoveAt(data.Count() - 1);
            // Insert the variable value into the beginning of the array
            data.Insert(0, toInsert);
        }
       

    }
}
