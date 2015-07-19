@pushd "%~dp0"

git clean -fdx src
nuget restore src
msbuild /nologo /v:minimal /m /fl src\Xunit.Combinatorial.sln /p:configuration=release,UseNonZeroBuildNumber=true
vstest.console.exe src\Xunit.Combinatorial.Tests\bin\release\Xunit.Combinatorial.Tests.dll /TestAdapterPath:packages

@echo Last steps to release:
@echo git tag v1.0.******
@echo git push origin v1.0.******
@echo nuget push src\Xunit.Combinatorial.NuGet\bin\release\Xunit.Combinatorial.*.nupkg

@popd
