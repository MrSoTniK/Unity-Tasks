public static class Shuffler
{
	public static T[] ShuffleArray<T>(T[] array)
	{
		System.Random random = new System.Random();

		for (int i = 0; i < array.Length - 1; i++)
		{
			int randomIndex = random.Next(i, array.Length);
			T tempItem = array[randomIndex];
			array[randomIndex] = array[i];
			array[i] = tempItem;
		}
		return array;
	}
}