namespace UI.Utilities
{
	public static class FileHelper
	{
		public static string? UploadImage(IWebHostEnvironment env, IFormFile formFile)
		{
			// we need to get the path of the wwwroot folder
			// creates a random unique id
			string guid = Guid.NewGuid().ToString();

			// get the file extension
			// we want to use guid.[extension]
			string ext = Path.GetExtension(formFile.FileName);


			//get the short path
			// path relative to our web server
			string shortPath = Path.Combine("assets\\Items", guid + ext);

			// get the full path
			// ex. C:\Users\user\source\repos\RazorCrudUI\wwwroot\assets\Items\guid.ext
			// using Path.Combine takes care of the slashes like \ or / that are different on different OS
			string fullPath = Path.Combine(env.WebRootPath, shortPath);

			// anytime we use a resource that is disposable, we should use a using statement
			// every time that we have a file stream, we should use a using statement
			using (var fileStream = new FileStream(fullPath, FileMode.Create))
			{
				formFile.CopyTo(fileStream);
			}

			return shortPath;
		}

		public static void DeleteOldImage(IWebHostEnvironment env, string? imageURL)
		{
			if (imageURL == null)
			{
				return;
			}
			string fullPath = Path.Combine(env.WebRootPath, imageURL);
			if (File.Exists(fullPath))
			{
				File.Delete(fullPath);

			}
		}
	}
}
