using System;
using System.Collections;
namespace myControl
{
	public class WordInfoSorter : IComparer
	{
		int IComparer.Compare(object x, object y)
		{
			int result;
			if (((WordInfo)x).m_fontsize < ((WordInfo)y).m_fontsize)
			{
				result = -1;
			}
			else if (((WordInfo)x).m_fontsize == ((WordInfo)y).m_fontsize)
			{
				result = 0;
			}
			else
			{
				result = 1;
			}
			return result;
		}
	}
}
