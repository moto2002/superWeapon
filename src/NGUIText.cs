using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public static class NGUIText
{
	public enum Alignment
	{
		Automatic,
		Left,
		Center,
		Right,
		Justified
	}

	public enum SymbolStyle
	{
		None,
		Normal,
		Colored
	}

	public class GlyphInfo
	{
		public Vector2 v0;

		public Vector2 v1;

		public Vector2 u0;

		public Vector2 u1;

		public float advance;

		public int channel;

		public bool rotatedUVs;
	}

	public static UIFont bitmapFont;

	public static Font dynamicFont;

	public static NGUIText.GlyphInfo glyph = new NGUIText.GlyphInfo();

	public static int fontSize = 16;

	public static float fontScale = 1f;

	public static float pixelDensity = 1f;

	public static FontStyle fontStyle = FontStyle.Normal;

	public static NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	public static Color tint = Color.white;

	public static int rectWidth = 1000000;

	public static int rectHeight = 1000000;

	public static int maxLines = 0;

	public static bool gradient = false;

	public static Color gradientBottom = Color.white;

	public static Color gradientTop = Color.white;

	public static bool encoding = false;

	public static float spacingX = 0f;

	public static float spacingY = 0f;

	public static bool premultiply = false;

	public static NGUIText.SymbolStyle symbolStyle;

	public static int finalSize = 0;

	public static float finalSpacingX = 0f;

	public static float finalLineHeight = 0f;

	public static float baseline = 0f;

	public static bool useSymbols = false;

	private static Color mInvisible = new Color(0f, 0f, 0f, 0f);

	private static BetterList<Color> mColors = new BetterList<Color>();

	private static CharacterInfo mTempChar;

	private static BetterList<float> mSizes = new BetterList<float>();

	private static Color32 s_c0;

	private static Color32 s_c1;

	private static float[] mBoldOffset = new float[]
	{
		-0.5f,
		0f,
		0.5f,
		0f,
		0f,
		-0.5f,
		0f,
		0.5f
	};

	public static void Update()
	{
		NGUIText.Update(true);
	}

	public static void Update(bool request)
	{
		NGUIText.finalSize = Mathf.RoundToInt((float)NGUIText.fontSize / NGUIText.pixelDensity);
		NGUIText.finalSpacingX = NGUIText.spacingX * NGUIText.fontScale;
		NGUIText.finalLineHeight = ((float)NGUIText.fontSize + NGUIText.spacingY) * NGUIText.fontScale;
		NGUIText.useSymbols = (NGUIText.bitmapFont != null && NGUIText.bitmapFont.hasSymbols && NGUIText.encoding && NGUIText.symbolStyle != NGUIText.SymbolStyle.None);
		if (NGUIText.dynamicFont != null && request)
		{
			NGUIText.dynamicFont.RequestCharactersInTexture(")_-", NGUIText.finalSize, NGUIText.fontStyle);
			if (!NGUIText.dynamicFont.GetCharacterInfo(')', out NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
			{
				NGUIText.dynamicFont.RequestCharactersInTexture("A", NGUIText.finalSize, NGUIText.fontStyle);
				if (!NGUIText.dynamicFont.GetCharacterInfo('A', out NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
				{
					NGUIText.baseline = 0f;
					return;
				}
			}
			float yMax = NGUIText.mTempChar.vert.yMax;
			float yMin = NGUIText.mTempChar.vert.yMin;
			NGUIText.baseline = Mathf.Round(yMax + ((float)NGUIText.finalSize - yMax + yMin) * 0.5f);
		}
	}

	public static void Prepare(string text)
	{
		if (NGUIText.dynamicFont != null)
		{
			NGUIText.dynamicFont.RequestCharactersInTexture(text, NGUIText.finalSize, NGUIText.fontStyle);
		}
	}

	public static BMSymbol GetSymbol(string text, int index, int textLength)
	{
		return (!(NGUIText.bitmapFont != null)) ? null : NGUIText.bitmapFont.MatchSymbol(text, index, textLength);
	}

	public static float GetGlyphWidth(int ch, int prev)
	{
		if (NGUIText.bitmapFont != null)
		{
			BMGlyph bMGlyph = NGUIText.bitmapFont.bmFont.GetGlyph(ch);
			if (bMGlyph != null)
			{
				return NGUIText.fontScale * (float)((prev == 0) ? bMGlyph.advance : (bMGlyph.advance + bMGlyph.GetKerning(prev)));
			}
		}
		else if (NGUIText.dynamicFont != null && NGUIText.dynamicFont.GetCharacterInfo((char)ch, out NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
		{
			return NGUIText.mTempChar.width * NGUIText.fontScale * NGUIText.pixelDensity;
		}
		return 0f;
	}

	public static NGUIText.GlyphInfo GetGlyph(int ch, int prev)
	{
		if (NGUIText.bitmapFont != null)
		{
			BMGlyph bMGlyph = NGUIText.bitmapFont.bmFont.GetGlyph(ch);
			if (bMGlyph != null)
			{
				int num = (prev == 0) ? 0 : bMGlyph.GetKerning(prev);
				NGUIText.glyph.v0.x = (float)((prev == 0) ? bMGlyph.offsetX : (bMGlyph.offsetX + num));
				NGUIText.glyph.v1.y = (float)(-(float)bMGlyph.offsetY);
				NGUIText.glyph.v1.x = NGUIText.glyph.v0.x + (float)bMGlyph.width;
				NGUIText.glyph.v0.y = NGUIText.glyph.v1.y - (float)bMGlyph.height;
				NGUIText.glyph.u0.x = (float)bMGlyph.x;
				NGUIText.glyph.u0.y = (float)(bMGlyph.y + bMGlyph.height);
				NGUIText.glyph.u1.x = (float)(bMGlyph.x + bMGlyph.width);
				NGUIText.glyph.u1.y = (float)bMGlyph.y;
				NGUIText.glyph.advance = (float)(bMGlyph.advance + num);
				NGUIText.glyph.channel = bMGlyph.channel;
				NGUIText.glyph.rotatedUVs = false;
				if (NGUIText.fontScale != 1f)
				{
					NGUIText.glyph.v0 *= NGUIText.fontScale;
					NGUIText.glyph.v1 *= NGUIText.fontScale;
					NGUIText.glyph.advance *= NGUIText.fontScale;
				}
				return NGUIText.glyph;
			}
		}
		else if (NGUIText.dynamicFont != null && NGUIText.dynamicFont.GetCharacterInfo((char)ch, out NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
		{
			NGUIText.glyph.v0.x = NGUIText.mTempChar.vert.xMin;
			NGUIText.glyph.v1.x = NGUIText.glyph.v0.x + NGUIText.mTempChar.vert.width;
			NGUIText.glyph.v0.y = NGUIText.mTempChar.vert.yMax - NGUIText.baseline;
			NGUIText.glyph.v1.y = NGUIText.glyph.v0.y - NGUIText.mTempChar.vert.height;
			NGUIText.glyph.u0.x = NGUIText.mTempChar.uv.xMin;
			NGUIText.glyph.u0.y = NGUIText.mTempChar.uv.yMin;
			NGUIText.glyph.u1.x = NGUIText.mTempChar.uv.xMax;
			NGUIText.glyph.u1.y = NGUIText.mTempChar.uv.yMax;
			NGUIText.glyph.advance = NGUIText.mTempChar.width;
			NGUIText.glyph.channel = 0;
			NGUIText.glyph.rotatedUVs = NGUIText.mTempChar.flipped;
			NGUIText.glyph.v0.x = Mathf.Round(NGUIText.glyph.v0.x);
			NGUIText.glyph.v0.y = Mathf.Round(NGUIText.glyph.v0.y);
			NGUIText.glyph.v1.x = Mathf.Round(NGUIText.glyph.v1.x);
			NGUIText.glyph.v1.y = Mathf.Round(NGUIText.glyph.v1.y);
			float num2 = NGUIText.fontScale * NGUIText.pixelDensity;
			if (num2 != 1f)
			{
				NGUIText.glyph.v0 *= num2;
				NGUIText.glyph.v1 *= num2;
				NGUIText.glyph.advance *= num2;
			}
			return NGUIText.glyph;
		}
		return null;
	}

	public static Color ParseColor(string text, int offset)
	{
		return NGUIText.ParseColor24(text, offset);
	}

	public static Color ParseColor24(string text, int offset)
	{
		int num = NGUIMath.HexToDecimal(text[offset]) << 4 | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = NGUIMath.HexToDecimal(text[offset + 2]) << 4 | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = NGUIMath.HexToDecimal(text[offset + 4]) << 4 | NGUIMath.HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	public static Color ParseColor32(string text, int offset)
	{
		int num = NGUIMath.HexToDecimal(text[offset]) << 4 | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = NGUIMath.HexToDecimal(text[offset + 2]) << 4 | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = NGUIMath.HexToDecimal(text[offset + 4]) << 4 | NGUIMath.HexToDecimal(text[offset + 5]);
		int num4 = NGUIMath.HexToDecimal(text[offset + 6]) << 4 | NGUIMath.HexToDecimal(text[offset + 7]);
		float num5 = 0.003921569f;
		return new Color(num5 * (float)num, num5 * (float)num2, num5 * (float)num3, num5 * (float)num4);
	}

	public static string EncodeColor(Color c)
	{
		return NGUIText.EncodeColor24(c);
	}

	public static string EncodeColor24(Color c)
	{
		int num = 16777215 & NGUIMath.ColorToInt(c) >> 8;
		return NGUIMath.DecimalToHex24(num);
	}

	public static string EncodeColor32(Color c)
	{
		int num = NGUIMath.ColorToInt(c);
		return NGUIMath.DecimalToHex32(num);
	}

	public static bool ParseSymbol(string text, ref int index)
	{
		int num = 1;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		return NGUIText.ParseSymbol(text, ref index, null, false, ref num, ref flag, ref flag2, ref flag3, ref flag4);
	}

	public static bool ParseSymbol(string text, ref int index, BetterList<Color> colors, bool premultiply, ref int sub, ref bool bold, ref bool italic, ref bool underline, ref bool strike)
	{
		int length = text.Length;
		if (index + 3 > length || text[index] != '[')
		{
			return false;
		}
		if (text[index + 2] == ']')
		{
			if (text[index + 1] == '-')
			{
				if (colors != null && colors.size > 1)
				{
					colors.RemoveAt(colors.size - 1);
				}
				index += 3;
				return true;
			}
			string text2 = text.Substring(index, 3);
			string text3 = text2;
			switch (text3)
			{
			case "[b]":
				bold = true;
				index += 3;
				return true;
			case "[i]":
				italic = true;
				index += 3;
				return true;
			case "[u]":
				underline = true;
				index += 3;
				return true;
			case "[s]":
				strike = true;
				index += 3;
				return true;
			}
		}
		if (index + 4 > length)
		{
			return false;
		}
		if (text[index + 3] == ']')
		{
			string text4 = text.Substring(index, 4);
			string text3 = text4;
			switch (text3)
			{
			case "[/b]":
				bold = false;
				index += 4;
				return true;
			case "[/i]":
				italic = false;
				index += 4;
				return true;
			case "[/u]":
				underline = false;
				index += 4;
				return true;
			case "[/s]":
				strike = false;
				index += 4;
				return true;
			}
		}
		if (index + 5 > length)
		{
			return false;
		}
		if (text[index + 4] == ']')
		{
			string text5 = text.Substring(index, 5);
			string text3 = text5;
			if (text3 != null)
			{
				if (NGUIText.<>f__switch$map5 == null)
				{
					NGUIText.<>f__switch$map5 = new Dictionary<string, int>(2)
					{
						{
							"[sub]",
							0
						},
						{
							"[sup]",
							1
						}
					};
				}
				int num;
				if (NGUIText.<>f__switch$map5.TryGetValue(text3, out num))
				{
					if (num == 0)
					{
						sub = 1;
						index += 5;
						return true;
					}
					if (num == 1)
					{
						sub = 2;
						index += 5;
						return true;
					}
				}
			}
		}
		if (index + 6 > length)
		{
			return false;
		}
		if (text[index + 5] == ']')
		{
			string text6 = text.Substring(index, 6);
			string text3 = text6;
			switch (text3)
			{
			case "[/sub]":
				sub = 0;
				index += 6;
				return true;
			case "[/sup]":
				sub = 0;
				index += 6;
				return true;
			case "[/url]":
				index += 6;
				return true;
			}
		}
		if (text[index + 1] == 'u' && text[index + 2] == 'r' && text[index + 3] == 'l' && text[index + 4] == '=')
		{
			int num2 = text.IndexOf(']', index + 4);
			if (num2 != -1)
			{
				index = num2 + 1;
				return true;
			}
		}
		if (index + 8 > length)
		{
			return false;
		}
		if (text[index + 7] == ']')
		{
			Color color = NGUIText.ParseColor24(text, index + 1);
			if (NGUIText.EncodeColor24(color) != text.Substring(index + 1, 6).ToUpper())
			{
				return false;
			}
			if (colors != null)
			{
				color.a = colors[colors.size - 1].a;
				if (premultiply && color.a != 1f)
				{
					color = Color.Lerp(NGUIText.mInvisible, color, color.a);
				}
				colors.Add(color);
			}
			index += 8;
			return true;
		}
		else
		{
			if (index + 10 > length)
			{
				return false;
			}
			if (text[index + 9] != ']')
			{
				return false;
			}
			Color color2 = NGUIText.ParseColor32(text, index + 1);
			if (NGUIText.EncodeColor32(color2) != text.Substring(index + 1, 8).ToUpper())
			{
				return false;
			}
			if (colors != null)
			{
				if (premultiply && color2.a != 1f)
				{
					color2 = Color.Lerp(NGUIText.mInvisible, color2, color2.a);
				}
				colors.Add(color2);
			}
			index += 10;
			return true;
		}
	}

	public static string StripSymbols(string text)
	{
		if (text != null)
		{
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				char c = text[i];
				if (c == '[')
				{
					int num = 0;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					int num2 = i;
					if (NGUIText.ParseSymbol(text, ref num2, null, false, ref num, ref flag, ref flag2, ref flag3, ref flag4))
					{
						text = text.Remove(i, num2 - i);
						length = text.Length;
						continue;
					}
				}
				i++;
			}
		}
		return text;
	}

	public static void Align(BetterList<Vector3> verts, int indexOffset, float printedWidth)
	{
		switch (NGUIText.alignment)
		{
		case NGUIText.Alignment.Center:
		{
			float num = ((float)NGUIText.rectWidth - printedWidth) * 0.5f;
			if (num < 0f)
			{
				return;
			}
			int num2 = Mathf.RoundToInt((float)NGUIText.rectWidth - printedWidth);
			int num3 = Mathf.RoundToInt((float)NGUIText.rectWidth);
			bool flag = (num2 & 1) == 1;
			bool flag2 = (num3 & 1) == 1;
			if ((flag && !flag2) || (!flag && flag2))
			{
				num += 0.5f * NGUIText.fontScale;
			}
			for (int i = indexOffset; i < verts.size; i++)
			{
				Vector3[] expr_F1_cp_0 = verts.buffer;
				int expr_F1_cp_1 = i;
				expr_F1_cp_0[expr_F1_cp_1].x = expr_F1_cp_0[expr_F1_cp_1].x + num;
			}
			break;
		}
		case NGUIText.Alignment.Right:
		{
			float num4 = (float)NGUIText.rectWidth - printedWidth;
			if (num4 < 0f)
			{
				return;
			}
			for (int j = indexOffset; j < verts.size; j++)
			{
				Vector3[] expr_49_cp_0 = verts.buffer;
				int expr_49_cp_1 = j;
				expr_49_cp_0[expr_49_cp_1].x = expr_49_cp_0[expr_49_cp_1].x + num4;
			}
			break;
		}
		case NGUIText.Alignment.Justified:
		{
			if (printedWidth < (float)NGUIText.rectWidth * 0.65f)
			{
				return;
			}
			float num5 = ((float)NGUIText.rectWidth - printedWidth) * 0.5f;
			if (num5 < 1f)
			{
				return;
			}
			int num6 = (verts.size - indexOffset) / 4;
			if (num6 < 1)
			{
				return;
			}
			float num7 = 1f / (float)(num6 - 1);
			float num8 = (float)NGUIText.rectWidth / printedWidth;
			int k = indexOffset + 4;
			int num9 = 1;
			while (k < verts.size)
			{
				float num10 = verts.buffer[k].x;
				float num11 = verts.buffer[k + 2].x;
				float num12 = num11 - num10;
				float num13 = num10 * num8;
				float from = num13 + num12;
				float num14 = num11 * num8;
				float to = num14 - num12;
				float t = (float)num9 * num7;
				num10 = Mathf.Lerp(num13, to, t);
				num11 = Mathf.Lerp(from, num14, t);
				num10 = Mathf.Round(num10);
				num11 = Mathf.Round(num11);
				verts.buffer[k++].x = num10;
				verts.buffer[k++].x = num10;
				verts.buffer[k++].x = num11;
				verts.buffer[k++].x = num11;
				num9++;
			}
			break;
		}
		}
	}

	public static int GetClosestCharacter(BetterList<Vector3> verts, Vector2 pos)
	{
		float num = 3.40282347E+38f;
		float num2 = 3.40282347E+38f;
		int result = 0;
		for (int i = 0; i < verts.size; i++)
		{
			float num3 = Mathf.Abs(pos.y - verts[i].y);
			if (num3 <= num2)
			{
				float num4 = Mathf.Abs(pos.x - verts[i].x);
				if (num3 < num2)
				{
					num2 = num3;
					num = num4;
					result = i;
				}
				else if (num4 < num)
				{
					num = num4;
					result = i;
				}
			}
		}
		return result;
	}

	[DebuggerHidden, DebuggerStepThrough]
	private static bool IsSpace(int ch)
	{
		return ch == 32 || ch == 8202 || ch == 8203;
	}

	[DebuggerHidden, DebuggerStepThrough]
	public static void EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && NGUIText.IsSpace((int)s[num]))
		{
			s[num] = '\n';
		}
		else
		{
			s.Append('\n');
		}
	}

	[DebuggerHidden, DebuggerStepThrough]
	private static void ReplaceSpaceWithNewline(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && NGUIText.IsSpace((int)s[num]))
		{
			s[num] = '\n';
		}
	}

	public static Vector2 CalculatePrintedSize(string text)
	{
		Vector2 zero = Vector2.zero;
		if (!string.IsNullOrEmpty(text))
		{
			if (NGUIText.encoding)
			{
				text = NGUIText.StripSymbols(text);
			}
			NGUIText.Prepare(text);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int length = text.Length;
			int prev = 0;
			for (int i = 0; i < length; i++)
			{
				int num4 = (int)text[i];
				if (num4 == 10)
				{
					if (num > num3)
					{
						num3 = num;
					}
					num = 0f;
					num2 += NGUIText.finalLineHeight;
				}
				else if (num4 >= 32)
				{
					BMSymbol bMSymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
					if (bMSymbol == null)
					{
						float num5 = NGUIText.GetGlyphWidth(num4, prev);
						if (num5 != 0f)
						{
							num5 += NGUIText.finalSpacingX;
							if (Mathf.RoundToInt(num + num5) > NGUIText.rectWidth)
							{
								if (num > num3)
								{
									num3 = num - NGUIText.finalSpacingX;
								}
								num = num5;
								num2 += NGUIText.finalLineHeight;
							}
							else
							{
								num += num5;
							}
							prev = num4;
						}
					}
					else
					{
						float num6 = NGUIText.finalSpacingX + (float)bMSymbol.advance * NGUIText.fontScale;
						if (Mathf.RoundToInt(num + num6) > NGUIText.rectWidth)
						{
							if (num > num3)
							{
								num3 = num - NGUIText.finalSpacingX;
							}
							num = num6;
							num2 += NGUIText.finalLineHeight;
						}
						else
						{
							num += num6;
						}
						i += bMSymbol.sequence.Length - 1;
						prev = 0;
					}
				}
			}
			zero.x = ((num <= num3) ? num3 : (num - NGUIText.finalSpacingX));
			zero.y = num2 + NGUIText.finalLineHeight;
		}
		return zero;
	}

	public static int CalculateOffsetToFit(string text)
	{
		if (string.IsNullOrEmpty(text) || NGUIText.rectWidth < 1)
		{
			return 0;
		}
		NGUIText.Prepare(text);
		int length = text.Length;
		int prev = 0;
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			BMSymbol bMSymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
			if (bMSymbol == null)
			{
				int num = (int)text[i];
				float glyphWidth = NGUIText.GetGlyphWidth(num, prev);
				if (glyphWidth != 0f)
				{
					NGUIText.mSizes.Add(NGUIText.finalSpacingX + glyphWidth);
				}
				prev = num;
			}
			else
			{
				NGUIText.mSizes.Add(NGUIText.finalSpacingX + (float)bMSymbol.advance * NGUIText.fontScale);
				int j = 0;
				int num2 = bMSymbol.sequence.Length - 1;
				while (j < num2)
				{
					NGUIText.mSizes.Add(0f);
					j++;
				}
				i += bMSymbol.sequence.Length - 1;
				prev = 0;
			}
			i++;
		}
		float num3 = (float)NGUIText.rectWidth;
		int num4 = NGUIText.mSizes.size;
		while (num4 > 0 && num3 > 0f)
		{
			num3 -= NGUIText.mSizes[--num4];
		}
		NGUIText.mSizes.Clear();
		if (num3 < 0f)
		{
			num4++;
		}
		return num4;
	}

	public static string GetEndOfLineThatFits(string text)
	{
		int length = text.Length;
		int num = NGUIText.CalculateOffsetToFit(text);
		return text.Substring(num, length - num);
	}

	public static bool WrapText(string text, out string finalText)
	{
		return NGUIText.WrapText(text, out finalText, false);
	}

	public static bool WrapText(string text, out string finalText, bool keepCharCount)
	{
		if (NGUIText.rectWidth < 1 || NGUIText.rectHeight < 1 || NGUIText.finalLineHeight < 1f)
		{
			finalText = string.Empty;
			return false;
		}
		float num = (NGUIText.maxLines <= 0) ? ((float)NGUIText.rectHeight) : Mathf.Min((float)NGUIText.rectHeight, NGUIText.finalLineHeight * (float)NGUIText.maxLines);
		int num2 = (NGUIText.maxLines <= 0) ? 1000000 : NGUIText.maxLines;
		num2 = Mathf.FloorToInt(Mathf.Min((float)num2, num / NGUIText.finalLineHeight) + 0.01f);
		if (num2 == 0)
		{
			finalText = string.Empty;
			return false;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		StringBuilder stringBuilder = new StringBuilder();
		int length = text.Length;
		float num3 = (float)NGUIText.rectWidth;
		int num4 = 0;
		int i = 0;
		int num5 = 1;
		int num6 = 0;
		bool flag = true;
		bool flag2 = true;
		bool flag3 = false;
		while (i < length)
		{
			char c = text[i];
			if (c > '⿿')
			{
				flag3 = true;
			}
			if (c == '\n')
			{
				if (num5 == num2)
				{
					break;
				}
				num3 = (float)NGUIText.rectWidth;
				if (num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					stringBuilder.Append(c);
				}
				flag = true;
				num5++;
				num4 = i + 1;
				num6 = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bMSymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
				float num7;
				if (bMSymbol == null)
				{
					float glyphWidth = NGUIText.GetGlyphWidth((int)c, num6);
					if (glyphWidth == 0f)
					{
						goto IL_3B3;
					}
					num7 = NGUIText.finalSpacingX + glyphWidth;
				}
				else
				{
					num7 = NGUIText.finalSpacingX + (float)bMSymbol.advance * NGUIText.fontScale;
				}
				num3 -= num7;
				if (NGUIText.IsSpace((int)c) && !flag3)
				{
					bool flag4 = NGUIText.IsSpace(num6);
					if (flag4)
					{
						stringBuilder.Append(' ');
						num4 = i;
					}
					else if (!flag4 && num4 < i)
					{
						int num8 = i - num4 + 1;
						if (num5 == num2 && num3 <= 0f && i < length)
						{
							char c2 = text[i];
							if (c2 < ' ' || NGUIText.IsSpace((int)c2))
							{
								num8--;
							}
						}
						stringBuilder.Append(text.Substring(num4, num8));
						flag = false;
						num4 = i + 1;
					}
				}
				if (Mathf.RoundToInt(num3) < 0)
				{
					if (flag || num5 == num2)
					{
						stringBuilder.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
						bool flag5 = NGUIText.IsSpace((int)c);
						if (!flag5 && !flag3)
						{
							flag2 = false;
						}
						if (num5++ == num2)
						{
							num4 = i;
							break;
						}
						if (keepCharCount)
						{
							NGUIText.ReplaceSpaceWithNewline(ref stringBuilder);
						}
						else
						{
							NGUIText.EndLine(ref stringBuilder);
						}
						flag = true;
						if (flag5)
						{
							num4 = i + 1;
							num3 = (float)NGUIText.rectWidth;
						}
						else
						{
							num4 = i;
							num3 = (float)NGUIText.rectWidth - num7;
						}
						num6 = 0;
					}
					else
					{
						flag = true;
						num3 = (float)NGUIText.rectWidth;
						i = num4 - 1;
						num6 = 0;
						if (num5++ == num2)
						{
							break;
						}
						if (keepCharCount)
						{
							NGUIText.ReplaceSpaceWithNewline(ref stringBuilder);
						}
						else
						{
							NGUIText.EndLine(ref stringBuilder);
						}
						goto IL_3B3;
					}
				}
				else
				{
					num6 = (int)c;
				}
				if (bMSymbol != null)
				{
					i += bMSymbol.length - 1;
					num6 = 0;
				}
			}
			IL_3B3:
			i++;
		}
		if (num4 < i)
		{
			stringBuilder.Append(text.Substring(num4, i - num4));
		}
		finalText = stringBuilder.ToString();
		return flag2 && (i == length || num5 <= Mathf.Min(NGUIText.maxLines, num2));
	}

	public static void Print(string text, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		int size = verts.size;
		NGUIText.Prepare(text);
		NGUIText.mColors.Add(Color.white);
		int prev = 0;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)NGUIText.finalSize;
		Color a = NGUIText.tint * NGUIText.gradientBottom;
		Color b = NGUIText.tint * NGUIText.gradientTop;
		Color32 color = NGUIText.tint;
		int length = text.Length;
		Rect rect = default(Rect);
		float num5 = 0f;
		float num6 = 0f;
		float num7 = num4 * NGUIText.pixelDensity;
		bool flag = false;
		int num8 = 0;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		if (NGUIText.bitmapFont != null)
		{
			rect = NGUIText.bitmapFont.uvRect;
			num5 = rect.width / (float)NGUIText.bitmapFont.texWidth;
			num6 = rect.height / (float)NGUIText.bitmapFont.texHeight;
		}
		for (int i = 0; i < length; i++)
		{
			int num9 = (int)text[i];
			float x = num;
			if (num9 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (NGUIText.alignment != NGUIText.Alignment.Left)
				{
					NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
					size = verts.size;
				}
				num = 0f;
				num2 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num9 < 32)
			{
				prev = num9;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i, NGUIText.mColors, NGUIText.premultiply, ref num8, ref flag2, ref flag3, ref flag4, ref flag5))
			{
				Color color2 = NGUIText.tint * NGUIText.mColors[NGUIText.mColors.size - 1];
				color = color2;
				if (NGUIText.gradient)
				{
					a = NGUIText.gradientBottom * color2;
					b = NGUIText.gradientTop * color2;
				}
				i--;
			}
			else
			{
				BMSymbol bMSymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
				if (bMSymbol != null)
				{
					float num10 = num + (float)bMSymbol.offsetX * NGUIText.fontScale;
					float num11 = num10 + (float)bMSymbol.width * NGUIText.fontScale;
					float num12 = -(num2 + (float)bMSymbol.offsetY * NGUIText.fontScale);
					float num13 = num12 - (float)bMSymbol.height * NGUIText.fontScale;
					if (Mathf.RoundToInt(num + (float)bMSymbol.advance * NGUIText.fontScale) > NGUIText.rectWidth)
					{
						if (num == 0f)
						{
							return;
						}
						if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
						{
							NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
							size = verts.size;
						}
						num10 -= num;
						num11 -= num;
						num13 -= NGUIText.finalLineHeight;
						num12 -= NGUIText.finalLineHeight;
						num = 0f;
						num2 += NGUIText.finalLineHeight;
					}
					verts.Add(new Vector3(num10, num13));
					verts.Add(new Vector3(num10, num12));
					verts.Add(new Vector3(num11, num12));
					verts.Add(new Vector3(num11, num13));
					num += NGUIText.finalSpacingX + (float)bMSymbol.advance * NGUIText.fontScale;
					i += bMSymbol.length - 1;
					prev = 0;
					if (uvs != null)
					{
						Rect uvRect = bMSymbol.uvRect;
						float xMin = uvRect.xMin;
						float yMin = uvRect.yMin;
						float xMax = uvRect.xMax;
						float yMax = uvRect.yMax;
						uvs.Add(new Vector2(xMin, yMin));
						uvs.Add(new Vector2(xMin, yMax));
						uvs.Add(new Vector2(xMax, yMax));
						uvs.Add(new Vector2(xMax, yMin));
					}
					if (cols != null)
					{
						if (NGUIText.symbolStyle == NGUIText.SymbolStyle.Colored)
						{
							for (int j = 0; j < 4; j++)
							{
								cols.Add(color);
							}
						}
						else
						{
							Color32 item = Color.white;
							item.a = color.a;
							for (int k = 0; k < 4; k++)
							{
								cols.Add(item);
							}
						}
					}
				}
				else
				{
					NGUIText.GlyphInfo glyphInfo = NGUIText.GetGlyph(num9, prev);
					if (glyphInfo != null)
					{
						prev = num9;
						if (num8 != 0)
						{
							NGUIText.GlyphInfo expr_456_cp_0 = glyphInfo;
							expr_456_cp_0.v0.x = expr_456_cp_0.v0.x * 0.75f;
							NGUIText.GlyphInfo expr_46E_cp_0 = glyphInfo;
							expr_46E_cp_0.v0.y = expr_46E_cp_0.v0.y * 0.75f;
							NGUIText.GlyphInfo expr_486_cp_0 = glyphInfo;
							expr_486_cp_0.v1.x = expr_486_cp_0.v1.x * 0.75f;
							NGUIText.GlyphInfo expr_49E_cp_0 = glyphInfo;
							expr_49E_cp_0.v1.y = expr_49E_cp_0.v1.y * 0.75f;
							if (num8 == 1)
							{
								NGUIText.GlyphInfo expr_4BE_cp_0 = glyphInfo;
								expr_4BE_cp_0.v0.y = expr_4BE_cp_0.v0.y - NGUIText.fontScale * (float)NGUIText.fontSize * 0.4f;
								NGUIText.GlyphInfo expr_4E3_cp_0 = glyphInfo;
								expr_4E3_cp_0.v1.y = expr_4E3_cp_0.v1.y - NGUIText.fontScale * (float)NGUIText.fontSize * 0.4f;
							}
							else
							{
								NGUIText.GlyphInfo expr_50D_cp_0 = glyphInfo;
								expr_50D_cp_0.v0.y = expr_50D_cp_0.v0.y + NGUIText.fontScale * (float)NGUIText.fontSize * 0.05f;
								NGUIText.GlyphInfo expr_532_cp_0 = glyphInfo;
								expr_532_cp_0.v1.y = expr_532_cp_0.v1.y + NGUIText.fontScale * (float)NGUIText.fontSize * 0.05f;
							}
						}
						float y = glyphInfo.v0.y;
						float y2 = glyphInfo.v1.y;
						float num10 = glyphInfo.v0.x + num;
						float num13 = glyphInfo.v0.y - num2;
						float num11 = glyphInfo.v1.x + num;
						float num12 = glyphInfo.v1.y - num2;
						float num14 = glyphInfo.advance;
						if (NGUIText.finalSpacingX < 0f)
						{
							num14 += NGUIText.finalSpacingX;
						}
						if (Mathf.RoundToInt(num + num14) > NGUIText.rectWidth)
						{
							if (num == 0f)
							{
								return;
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
							{
								NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
								size = verts.size;
							}
							num10 -= num;
							num11 -= num;
							num13 -= NGUIText.finalLineHeight;
							num12 -= NGUIText.finalLineHeight;
							num = 0f;
							num2 += NGUIText.finalLineHeight;
							x = 0f;
						}
						if (NGUIText.IsSpace(num9))
						{
							if (flag4)
							{
								num9 = 95;
							}
							else if (flag5)
							{
								num9 = 45;
							}
						}
						num += ((num8 != 0) ? ((NGUIText.finalSpacingX + glyphInfo.advance) * 0.75f) : (NGUIText.finalSpacingX + glyphInfo.advance));
						if (!NGUIText.IsSpace(num9))
						{
							if (uvs != null)
							{
								if (NGUIText.bitmapFont != null)
								{
									glyphInfo.u0.x = rect.xMin + num5 * glyphInfo.u0.x;
									glyphInfo.u1.x = rect.xMin + num5 * glyphInfo.u1.x;
									glyphInfo.u0.y = rect.yMax - num6 * glyphInfo.u0.y;
									glyphInfo.u1.y = rect.yMax - num6 * glyphInfo.u1.y;
								}
								int l = 0;
								int num15 = (!flag2) ? 1 : 4;
								while (l < num15)
								{
									if (glyphInfo.rotatedUVs)
									{
										uvs.Add(glyphInfo.u0);
										uvs.Add(new Vector2(glyphInfo.u1.x, glyphInfo.u0.y));
										uvs.Add(glyphInfo.u1);
										uvs.Add(new Vector2(glyphInfo.u0.x, glyphInfo.u1.y));
									}
									else
									{
										uvs.Add(glyphInfo.u0);
										uvs.Add(new Vector2(glyphInfo.u0.x, glyphInfo.u1.y));
										uvs.Add(glyphInfo.u1);
										uvs.Add(new Vector2(glyphInfo.u1.x, glyphInfo.u0.y));
									}
									l++;
								}
							}
							if (cols != null)
							{
								if (glyphInfo.channel == 0 || glyphInfo.channel == 15)
								{
									if (NGUIText.gradient)
									{
										float num16 = num7 + y / NGUIText.fontScale;
										float num17 = num7 + y2 / NGUIText.fontScale;
										num16 /= num7;
										num17 /= num7;
										NGUIText.s_c0 = Color.Lerp(a, b, num16);
										NGUIText.s_c1 = Color.Lerp(a, b, num17);
										int m = 0;
										int num18 = (!flag2) ? 1 : 4;
										while (m < num18)
										{
											cols.Add(NGUIText.s_c0);
											cols.Add(NGUIText.s_c1);
											cols.Add(NGUIText.s_c1);
											cols.Add(NGUIText.s_c0);
											m++;
										}
									}
									else
									{
										int n = 0;
										int num19 = (!flag2) ? 4 : 16;
										while (n < num19)
										{
											cols.Add(color);
											n++;
										}
									}
								}
								else
								{
									Color color3 = color;
									color3 *= 0.49f;
									switch (glyphInfo.channel)
									{
									case 1:
										color3.b += 0.51f;
										break;
									case 2:
										color3.g += 0.51f;
										break;
									case 4:
										color3.r += 0.51f;
										break;
									case 8:
										color3.a += 0.51f;
										break;
									}
									Color32 item2 = color3;
									int num20 = 0;
									int num21 = (!flag2) ? 4 : 16;
									while (num20 < num21)
									{
										cols.Add(item2);
										num20++;
									}
								}
							}
							if (!flag2)
							{
								if (!flag3)
								{
									verts.Add(new Vector3(num10, num13));
									verts.Add(new Vector3(num10, num12));
									verts.Add(new Vector3(num11, num12));
									verts.Add(new Vector3(num11, num13));
								}
								else
								{
									float num22 = (float)NGUIText.fontSize * 0.1f * ((num12 - num13) / (float)NGUIText.fontSize);
									verts.Add(new Vector3(num10 - num22, num13));
									verts.Add(new Vector3(num10 + num22, num12));
									verts.Add(new Vector3(num11 + num22, num12));
									verts.Add(new Vector3(num11 - num22, num13));
								}
							}
							else
							{
								for (int num23 = 0; num23 < 4; num23++)
								{
									float num24 = NGUIText.mBoldOffset[num23 * 2];
									float num25 = NGUIText.mBoldOffset[num23 * 2 + 1];
									float num26 = num24 + ((!flag3) ? 0f : ((float)NGUIText.fontSize * 0.1f * ((num12 - num13) / (float)NGUIText.fontSize)));
									verts.Add(new Vector3(num10 - num26, num13 + num25));
									verts.Add(new Vector3(num10 + num26, num12 + num25));
									verts.Add(new Vector3(num11 + num26, num12 + num25));
									verts.Add(new Vector3(num11 - num26, num13 + num25));
								}
							}
							if (flag4 || flag5)
							{
								NGUIText.GlyphInfo glyphInfo2 = NGUIText.GetGlyph((!flag5) ? 95 : 45, prev);
								if (glyphInfo2 != null)
								{
									if (uvs != null)
									{
										if (NGUIText.bitmapFont != null)
										{
											glyphInfo2.u0.x = rect.xMin + num5 * glyphInfo2.u0.x;
											glyphInfo2.u1.x = rect.xMin + num5 * glyphInfo2.u1.x;
											glyphInfo2.u0.y = rect.yMax - num6 * glyphInfo2.u0.y;
											glyphInfo2.u1.y = rect.yMax - num6 * glyphInfo2.u1.y;
										}
										float x2 = (glyphInfo2.u0.x + glyphInfo2.u1.x) * 0.5f;
										float y3 = (glyphInfo2.u0.y + glyphInfo2.u1.y) * 0.5f;
										uvs.Add(new Vector2(x2, y3));
										uvs.Add(new Vector2(x2, y3));
										uvs.Add(new Vector2(x2, y3));
										uvs.Add(new Vector2(x2, y3));
									}
									if (flag && flag5)
									{
										num13 = (-num2 + glyphInfo2.v0.y) * 0.75f;
										num12 = (-num2 + glyphInfo2.v1.y) * 0.75f;
									}
									else
									{
										num13 = -num2 + glyphInfo2.v0.y;
										num12 = -num2 + glyphInfo2.v1.y;
									}
									verts.Add(new Vector3(x, num13));
									verts.Add(new Vector3(x, num12));
									verts.Add(new Vector3(num, num12));
									verts.Add(new Vector3(num, num13));
									Color c = color;
									if (flag5)
									{
										c.r *= 0.5f;
										c.g *= 0.5f;
										c.b *= 0.5f;
									}
									c.a *= 0.75f;
									Color32 item3 = c;
									cols.Add(item3);
									cols.Add(color);
									cols.Add(color);
									cols.Add(item3);
								}
							}
						}
					}
				}
			}
		}
		if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
		{
			NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
			size = verts.size;
		}
		NGUIText.mColors.Clear();
	}

	public static void PrintCharacterPositions(string text, BetterList<Vector3> verts, BetterList<int> indices)
	{
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)NGUIText.fontSize * NGUIText.fontScale * 0.5f;
		int length = text.Length;
		int size = verts.size;
		int prev = 0;
		for (int i = 0; i < length; i++)
		{
			int num5 = (int)text[i];
			verts.Add(new Vector3(num, -num2 - num4));
			indices.Add(i);
			if (num5 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (NGUIText.alignment != NGUIText.Alignment.Left)
				{
					NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
					size = verts.size;
				}
				num = 0f;
				num2 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num5 < 32)
			{
				prev = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bMSymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
				if (bMSymbol == null)
				{
					float num6 = NGUIText.GetGlyphWidth(num5, prev);
					if (num6 != 0f)
					{
						num6 += NGUIText.finalSpacingX;
						if (Mathf.RoundToInt(num + num6) > NGUIText.rectWidth)
						{
							if (num == 0f)
							{
								return;
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
							{
								NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
								size = verts.size;
							}
							num = num6;
							num2 += NGUIText.finalLineHeight;
						}
						else
						{
							num += num6;
						}
						verts.Add(new Vector3(num, -num2 - num4));
						indices.Add(i + 1);
						prev = num5;
					}
				}
				else
				{
					float num7 = (float)bMSymbol.advance * NGUIText.fontScale + NGUIText.finalSpacingX;
					if (Mathf.RoundToInt(num + num7) > NGUIText.rectWidth)
					{
						if (num == 0f)
						{
							return;
						}
						if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
						{
							NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
							size = verts.size;
						}
						num = num7;
						num2 += NGUIText.finalLineHeight;
					}
					else
					{
						num += num7;
					}
					verts.Add(new Vector3(num, -num2 - num4));
					indices.Add(i + 1);
					i += bMSymbol.sequence.Length - 1;
					prev = 0;
				}
			}
		}
		if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
		{
			NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
		}
	}

	public static void PrintCaretAndSelection(string text, int start, int end, BetterList<Vector3> caret, BetterList<Vector3> highlight)
	{
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		int num = end;
		if (start > end)
		{
			end = start;
			start = num;
		}
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = (float)NGUIText.fontSize * NGUIText.fontScale;
		int indexOffset = (caret == null) ? 0 : caret.size;
		int num6 = (highlight == null) ? 0 : highlight.size;
		int length = text.Length;
		int i = 0;
		int prev = 0;
		bool flag = false;
		bool flag2 = false;
		Vector2 zero = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		while (i < length)
		{
			if (caret != null && !flag2 && num <= i)
			{
				flag2 = true;
				caret.Add(new Vector3(num2 - 1f, -num3 - num5));
				caret.Add(new Vector3(num2 - 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3 - num5));
			}
			int num7 = (int)text[i];
			if (num7 == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (caret != null && flag2)
				{
					if (NGUIText.alignment != NGUIText.Alignment.Left)
					{
						NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
					}
					caret = null;
				}
				if (highlight != null)
				{
					if (flag)
					{
						flag = false;
						highlight.Add(zero2);
						highlight.Add(zero);
					}
					else if (start <= i && end > i)
					{
						highlight.Add(new Vector3(num2, -num3 - num5));
						highlight.Add(new Vector3(num2, -num3));
						highlight.Add(new Vector3(num2 + 2f, -num3));
						highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
					}
					if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
					{
						NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
						num6 = highlight.size;
					}
				}
				num2 = 0f;
				num3 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num7 < 32)
			{
				prev = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bMSymbol = (!NGUIText.useSymbols) ? null : NGUIText.GetSymbol(text, i, length);
				float num8 = (bMSymbol == null) ? NGUIText.GetGlyphWidth(num7, prev) : ((float)bMSymbol.advance * NGUIText.fontScale);
				if (num8 != 0f)
				{
					float num9 = num2;
					float num10 = num2 + num8;
					float num11 = -num3 - num5;
					float num12 = -num3;
					if (Mathf.RoundToInt(num10 + NGUIText.finalSpacingX) > NGUIText.rectWidth)
					{
						if (num2 == 0f)
						{
							return;
						}
						if (num2 > num4)
						{
							num4 = num2;
						}
						if (caret != null && flag2)
						{
							if (NGUIText.alignment != NGUIText.Alignment.Left)
							{
								NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
							}
							caret = null;
						}
						if (highlight != null)
						{
							if (flag)
							{
								flag = false;
								highlight.Add(zero2);
								highlight.Add(zero);
							}
							else if (start <= i && end > i)
							{
								highlight.Add(new Vector3(num2, -num3 - num5));
								highlight.Add(new Vector3(num2, -num3));
								highlight.Add(new Vector3(num2 + 2f, -num3));
								highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
							{
								NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
								num6 = highlight.size;
							}
						}
						num9 -= num2;
						num10 -= num2;
						num11 -= NGUIText.finalLineHeight;
						num12 -= NGUIText.finalLineHeight;
						num2 = 0f;
						num3 += NGUIText.finalLineHeight;
					}
					num2 += num8 + NGUIText.finalSpacingX;
					if (highlight != null)
					{
						if (start > i || end <= i)
						{
							if (flag)
							{
								flag = false;
								highlight.Add(zero2);
								highlight.Add(zero);
							}
						}
						else if (!flag)
						{
							flag = true;
							highlight.Add(new Vector3(num9, num11));
							highlight.Add(new Vector3(num9, num12));
						}
					}
					zero = new Vector2(num10, num11);
					zero2 = new Vector2(num10, num12);
					prev = num7;
				}
			}
			i++;
		}
		if (caret != null)
		{
			if (!flag2)
			{
				caret.Add(new Vector3(num2 - 1f, -num3 - num5));
				caret.Add(new Vector3(num2 - 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3 - num5));
			}
			if (NGUIText.alignment != NGUIText.Alignment.Left)
			{
				NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
			}
		}
		if (highlight != null)
		{
			if (flag)
			{
				highlight.Add(zero2);
				highlight.Add(zero);
			}
			else if (start < i && end == i)
			{
				highlight.Add(new Vector3(num2, -num3 - num5));
				highlight.Add(new Vector3(num2, -num3));
				highlight.Add(new Vector3(num2 + 2f, -num3));
				highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
			}
			if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
			{
				NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
			}
		}
	}
}
