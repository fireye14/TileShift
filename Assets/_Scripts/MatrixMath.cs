using UnityEngine;
using System.Collections;

public class MatrixMath : MonoBehaviour 
{

	/// <summary>
	/// Checks to see if two integer arrays contain the same elements
	/// </summary>
	/// <returns>
	/// 		<c>true</c>  if the two arrays are equal 
	///         <c>false</c> otherwise
	/// </returns>
	/// 
	/// <param name="a1">The first array</param>
	/// <param name="a2">The second array</param>
	public static bool CompareArray(int[] a1, int[] a2)
	{
		//do the two arrays contain the same elements?

		//false if they are not the same length
		if(a1.Length != a2.Length) return false;

		//compare element by element
		for(int i = 0; i < a1.Length; i++)
		{
			//if we find two that are not equal, immediately return false
			if(a1[i] != a2[i]) return false;
		}

		//haven't returned false after checking all the elements: they are equal
		return true;
	}



	/// <summary>
	/// Subtracts int array a2 from int array a1, treating them as vectors (a1 - a2)
	/// </summary>
	/// <returns>The result of the subtraction</returns>
	/// <param name="a1">First array</param>
	/// <param name="a2">Second array</param>
	/// <param name="x">Integer to mod by</param>
	public static int[] SubtractArrayModX(int[] a1, int[] a2, int x)
	{
		//subtracts a2 from a1    ( a1 - a2 )

		//make sure arrays are the same size
		if(a1.Length != a2.Length) throw new UnityException("Arrays are not the same length");

		//store the result in a new array
		int[] result = new int[a1.Length];

		//loop through each element
		for(int i = 0; i < result.Length; i++)
		{
			//subtract this element of a2 from this element of a1, then mod by x
			result[i] = (a1[i] - a2[i]) % x;

			//if the result is negative, make sure it's positive
			//ex. (1 - 2) % 3 = -1...
			//  but in the number space Z mod 3, 1 - 2 = 2
			//  so, x is added to the result until it is non-negative
			while(result[i] < 0) result[i] += x;
		}

		return result;
	}



	/// <summary>
	/// Swaps two given rows in an int[rows, cols] matrix	
	/// </summary>
	/// <returns>The matrix after the swap </returns>
	/// <param name="m">The matrix</param>
	/// <param name="r1">The first row</param>
	/// <param name="r2">The second row</param>
	public static int[,] RowSwap(int[,] m, int r1, int r2)
	{
		//swaps r2 and r1

		//create a temporary int variable
		int temp;

		//iterate through each column in the row
		for(int i = 0; i <= m.GetUpperBound(1); i++)
		{
			//store the value in row1 in temp,
			temp = m[r1, i];

			//set the row1 value equal to the row2 value
			m[r1, i] = m[r2, i];

			//set the row2 value equal to temp
			m[r2, i] = temp;
		}

		//the rows have now been swapped
		return m;
	}



	/// <summary>
	/// Adds two rows of a given int[rows, cols] matrix, then replaces r2 with the result
	/// </summary>
	/// <returns>The matrix after the operation</returns>
	/// <param name="m">The matrix</param>
	/// <param name="r1">The first row</param>
	/// <param name="r2">The second row</param>
	/// <param name="x">The integer to mod by</param>
	public static int[,] RowAddModX(int[,] m, int r1, int r2, int x)
	{
		//adds row1 to row2 in the matrix

		//iterate through each column in the rows
		for(int i = 0; i <= m.GetUpperBound(1); i++)
		{
			//add this value in row1 to the value in row2, and mod by x
			m[r2, i] = (m[r1, i] + m[r2, i]) % x;
		}

		//the rows have now been added
		return m;

	}


	/// <summary>
	/// Multiplies a row in an int[rows, cols] matrix by a certain number, while modding by x	
    /// </summary>
	/// <returns>The matrix after the row has been multiplied </returns>
	/// <param name="m">The matrix</param>
	/// <param name="row">The row</param>
	/// <param name="mult">The number to multiply row by</param>
	/// <param name="x">The integer to mod by</param>
	public static int[,] MultiplyRowModX(int[,] m, int row, int mult, int x)
	{
		//iterate through each element in the row
		for(int i = 0; i <= m.GetUpperBound(1); i++)
		{
			//multiply this value by mult, mod by x, then store the new value
			m[row, i] = (m[row, i] * mult) % x;
		}

		//the row has been multiplied
		return m;

	}



	/// <summary>
	/// Puts the given int[rows, cols] matrix into reduced row echeleon form, in the Z(integers) mod x space
	/// </summary>
	/// <returns>The matrix after the RREF operation</returns>
	/// <param name="m">The matrix</param>
	/// <param name="x">The integer to mod by</param>
	public static int[,] RRefModX(int[,] m, int x)
	{
		//get number of rows and columns
		int rows = m.GetUpperBound(0);
				
		//loop through each row
		for(int i = 0; i <= rows; i++)
		{
			//check for a 0 in the diagonal
			//we want this to be nonzero
			if(m[i, i] == 0)
			{
				for(int j = i+1; j <= rows; j++)
				{
					//look for a nonzero number in this row
					if(m[j, i] != 0) 
					{
						//found a nonzero number in this row, swap i-th and j-th rows
						//so that m[i, i] (pivot entry) is nonzero
						m = RowSwap(m, i, j);

						//break out of the for loop
						break;
					}
				}
			}

			//if the diagonal still equals 0 (couldn't find a nonzero anywhere in its column) go onto next iteration
			//OR if x is a multiple of 5 AND we're on the last row
			if(m[i, i] == 0 || (x % 5 == 0 && x - m[i, i] == 5))   continue;

			//Due to the way this rref function works (don't know the exact specifics), the last row's pivot will be a special case
			//where it will be equal to x - 5. The following if, else if structure handles each of those special cases up to x = 11.
			//The final else if condition handles every other case, where the current row's pivot is equal to x - 1 (if it's not 
			//already equal to 1).
			if     (x ==  7 && m[i, i] == 2) m = MultiplyRowModX(m, i, 4, x);
			else if(x ==  8 && m[i, i] == 3) m = MultiplyRowModX(m, i, 3, x);
			else if(x ==  9 && m[i, i] == 4) m = MultiplyRowModX(m, i, 7, x);
			else if(x == 11 && m[i, i] == 6) m = MultiplyRowModX(m, i, 2, x);
			else if           (m[i, i] != 1) m = MultiplyRowModX(m, i, m[i, i], x);
			

			//iterate through each row above and below the i-th row, but skip the i-th row
			for(int j = 0; j <= rows; j++)
			{
				//don't want to mess with the i-th row, continue
				if(j == i) continue;
				
				//want this value to be 0, so add the i-th row to this row until it is 0
				while(m[j, i] != 0) m = RowAddModX (m, i, j, x);

			}
		}

		//the matrix is now in reduced row echelon form in mod x
		return m;
	}

}