d# Contact-Database Demo

https://github.com/yehiatarek63/Contact-Database/assets/94568731/1fb3d544-ee45-4ac2-a4fa-a0398e93600d

This is a contact form using edgedb that allows the user to create a contact and shows the list of contacts. It also has an onclick search feature using htmx.
# How to run the sample:
<ul>
  <li>Go to the Contact-Database folder</li>
  <li>Enter this command: edgedb project init --server-instance Contact-Database</li>
  <li>Press Enter on the prompts that follow</li>
  <li>Go to the second ContactDatabase folder (The one that has the .csproj)</li>
  <li>Type dotnet watch</li>
</ul>

# How to cleanup:
Run this command line
edgedb instance destroy -I Contact-Database  --force 
