@echo off
echo Deleting previous Allure results...
rmdir /s /q .\bin\Debug\net9.0\allure-results

echo Running tests...
dotnet test --logger:"trx" --results-directory ./bin/Debug/net9.0/allure-results
@REM dotnet test --filter FullyQualifiedName~Login --logger:"trx" --results-directory ./bin/Debug/net9.0/allure-results

echo Serving Allure report...
allure serve ./bin/Debug/net9.0/allure-results

