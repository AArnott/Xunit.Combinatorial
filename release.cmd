@pushd "%~dp0"

git clean -fdx src
nuget restore src
msbuild /nologo /v:minimal /m /fl src\Xunit.Combinatorial.sln /p:configuration=release,UseNonZeroBuildNumber=true
vstest.console.exe src\Xunit.Combinatorial.Tests\bin\release\Xunit.Combinatorial.Tests.dll /TestAdapterPath:packages

:: Delete the symbols package because we don't use it and if it's nearby the
:: primary package when we push, nuget.exe tries to push it too, resulting in
:: a long delay followed by an error because we don't include sources.
del src\Xunit.Combinatorial.NuGet\bin\release\*symbols.nupkg

@echo Last steps to release:
@echo git tag v1.0.******
@echo git push origin v1.0.******
@echo nuget push src\Xunit.Combinatorial.NuGet\bin\release\Xunit.Combinatorial.*.nupkg

@popd
