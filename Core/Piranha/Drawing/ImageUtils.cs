/*
 * Copyright (c) 2011-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Piranha.Models;

namespace Piranha.Drawing
{
	/// <summary>
	/// Image utils for scaling and cropping images.
	/// </summary>
	public static class ImageUtils
	{
		/// <summary>
		/// Resizes the given image to the given size. If the size is larger than the original the
		/// image is returned unchanged.
		/// </summary>
		/// <param name="img">The image to resize</param>
		/// <param name="width">The desired width</param>
		/// <returns>The resized image</returns>
		public static Image Resize(Image img, int width) {
			if (width < img.Width) {
				int height = Convert.ToInt32(((double)width / img.Width) * img.Height);

				using (Bitmap bmp = new Bitmap(width, height)) {
					Graphics grp = Graphics.FromImage(bmp);

					grp.SmoothingMode = SmoothingMode.HighQuality;
					grp.CompositingQuality = CompositingQuality.HighQuality;
					grp.InterpolationMode = InterpolationMode.High;

					// Resize and crop image
					Rectangle dst = new Rectangle(0, 0, bmp.Width, bmp.Height);
					grp.DrawImage(img, dst, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

					// Save new image to memory
					MemoryStream mem = new MemoryStream();
					bmp.Save(mem, img.RawFormat);
					mem.Position = 0;
					grp.Dispose();

					return Image.FromStream(mem);
				}
			}
			return img;
		}

		/// <summary>
		/// Resizes and crops the image to the given dimensions
		/// </summary>
		/// <param name="img">The image</param>
		/// <param name="width">The desired width</param>
		/// <param name="height">The desired height</param>
		/// <returns>The resized image</returns>
		public static Image Resize(Image img, int width, int height, HorizontalCropping cropH, VerticalCropping cropV) {
			// We only reduce size and crop, we don't magnify images
			if (!(img.Width == width && img.Height == height)) {
				if (width <= img.Width && height <= img.Height) {
                    var xRecPos = 0;
                    var yRecPos = 0;
                    var xRatio = width / (double)img.Width;
					var yRatio = height / (double)img.Height;

                    if (img.Height * xRatio < height)
                        img = Resize(img, Convert.ToInt32(img.Width * yRatio));
                    else img = Resize(img, Convert.ToInt32(width));

                    if (cropH == HorizontalCropping.Center)
                        xRecPos = (img.Width - width) / 2;
                    else if (cropH == HorizontalCropping.Right)
                        xRecPos = img.Width - width;

                    if (cropV == VerticalCropping.Center)
                        yRecPos = (img.Height - height) / 2;
                    else if (cropV == VerticalCropping.Bottom)
                        yRecPos = img.Height - height;

                    var newRect = new Rectangle(xRecPos, yRecPos, width, height);
                    var orgBmp = new Bitmap(img);
					var crpBmp = orgBmp.Clone(newRect, img.PixelFormat);
					orgBmp.Dispose();

					return (Image)crpBmp;
				}
			}
			return img;
		}
	}
}
