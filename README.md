<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>

    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="Moq" Version="4.20.70" />
    <PackageReference Include="xunit" Version="2.5.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Ecommerce.Service\Ecommerce.Service.csproj" />
    <ProjectReference Include="..\Ecommerce.Core\Ecommerce.Core.csproj" />
  </ItemGroup>

</Project>


<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>


<Project Sdk="Microsoft.NET.Sdk">

  <ItemGroup>
    <ProjectReference Include="..\Eshop.Service\Eshop.Service.csproj" />
    <ProjectReference Include="..\Eshop.Core\Eshop.Core.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.2.0" />
  </ItemGroup>

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>


<Project Sdk="Microsoft.NET.Sdk">

  <ItemGroup>
    <ProjectReference Include="..\Eshop.Core\Eshop.Core.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="AutoMapper" Version="12.0.1" />
    <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
  </ItemGroup>


  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>


<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="CsvHelper" Version="32.0.3" />
    <PackageReference Include="DotNetEnv" Version="3.0.0" />
    <PackageReference Include="EFCore.NamingConventions" Version="8.0.3" />
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.4" />
    <PackageReference Include="Microsoft.AspNetCore.Identity" Version="2.2.0" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.4">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.4">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.2" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.5.1" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Eshop.Controller\Eshop.Controller.csproj" />
    <ProjectReference Include="..\Eshop.Service\Eshop.Service.csproj" />
    <ProjectReference Include="..\Eshop.Core\Eshop.Core.csproj" />
  </ItemGroup>

</Project>

soln : 
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Eshop.Core", "Eshop.Core\Eshop.Core.csproj", "{33307B86-EF58-4C17-9DF9-C15AC20D8747}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Eshop.Service", "Eshop.Service\Eshop.Service.csproj", "{DE522346-4E0F-42D2-873C-7182879CE4BB}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Eshop.Controller", "Eshop.Controller\Eshop.Controller.csproj", "{61BD9202-20BC-4B02-A41D-915D6C2B48D7}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Eshop.WebAPI", "Eshop.WebAPI\Eshop.WebAPI.csproj", "{17A93BA7-6677-4CDD-8311-77CDF4245F44}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{33307B86-EF58-4C17-9DF9-C15AC20D8747}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{33307B86-EF58-4C17-9DF9-C15AC20D8747}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{33307B86-EF58-4C17-9DF9-C15AC20D8747}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{33307B86-EF58-4C17-9DF9-C15AC20D8747}.Release|Any CPU.Build.0 = Release|Any CPU
		{DE522346-4E0F-42D2-873C-7182879CE4BB}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{DE522346-4E0F-42D2-873C-7182879CE4BB}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{DE522346-4E0F-42D2-873C-7182879CE4BB}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{DE522346-4E0F-42D2-873C-7182879CE4BB}.Release|Any CPU.Build.0 = Release|Any CPU
		{61BD9202-20BC-4B02-A41D-915D6C2B48D7}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{61BD9202-20BC-4B02-A41D-915D6C2B48D7}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{61BD9202-20BC-4B02-A41D-915D6C2B48D7}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{61BD9202-20BC-4B02-A41D-915D6C2B48D7}.Release|Any CPU.Build.0 = Release|Any CPU
		{17A93BA7-6677-4CDD-8311-77CDF4245F44}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{17A93BA7-6677-4CDD-8311-77CDF4245F44}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{17A93BA7-6677-4CDD-8311-77CDF4245F44}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{17A93BA7-6677-4CDD-8311-77CDF4245F44}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal


this project is cleadn solid arcitect dotnet project. u need to write raed me file