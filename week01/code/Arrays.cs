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
       // Plan:
            // 1. Create an array containing 'length' positions.
            // 2. Loop through every index from 0 to length - 1.
            // 3. Store number * (index + 1) at each position.
            // 4. Return the completed array.

        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }
        return result;
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
    // Plan:
    // 1. Find where the last 'amount' items begin.
    // 2. Copy those items into a temporary list.
    // 3. Remove them from the original list.
    // 4. Insert them at the beginning, preserving their order.

    int startingIndex = data.Count - amount;

    List<int> itemsToMove = data.GetRange(startingIndex, amount);

    data.RemoveRange(startingIndex, amount);

    data.InsertRange(0, itemsToMove);
}
}
