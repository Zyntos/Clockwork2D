// Kevin Hagen
// 10.08.2018

using System.Text;
using UnityEngine;

namespace Utility
{
	public static class StringExtensions
	{
		public static string InColor(this string text, Color color)
		{
			StringBuilder builder = new StringBuilder(text.Length + 25);
			builder.Append("<color=#");
			builder.Append(ColorUtility.ToHtmlStringRGBA(color));
			builder.Append(">");
			builder.Append(text);
			builder.Append("</color>");

			return builder.ToString();
		}

		public static string InRed(this string text)
		{
			return text.InColor(Color.red);
		}

		public static string InBlue(this string text)
		{
			return text.InColor(Color.blue);
		}

		public static string InGreen(this string text)
		{
			return text.InColor(Color.green);
		}

		public static string InYellow(this string text)
		{
			return text.InColor(Color.yellow);
		}

		public static string InBold(this string text)
		{
			return "<b>" + text + "</b>";
		}

		public static string InItalic(this string text)
		{
			return "<i>" + text + "</i>";
		}
	}
}
