# payfurl/dotnetsdk. `just` lists the recipes.

# The format check, build and unit tests, as CI runs them
test:
    dotnet format PayfurlSdk.sln --verify-no-changes
    dotnet test PayfurlSdk.sln

# Apply the code style (.editorconfig)
format:
    dotnet format PayfurlSdk.sln

# Pack as the release workflow does, into nupkg/
pack version="0.0.0-local":
    dotnet pack src/PayfurlSdk -c Release -p:Version={{version}} -o nupkg

# The integration tests, in payfurl/platform, against this checkout and an API built from source (Docker); args go to dotnet test
e2e *args:
    #!/usr/bin/env bash
    set -euo pipefail
    sdk=$(pwd)
    platform=${PLATFORM_DIR:-}
    if [ -z "$platform" ]; then
        for candidate in ../platform ../../platform; do
            if [ -f "$candidate/e2e/api-tests/dotnetsdk/docker-compose.yml" ]; then platform=$candidate; break; fi
        done
    fi
    [ -n "$platform" ] && [ -f "$platform/e2e/api-tests/dotnetsdk/docker-compose.yml" ] \
        || { echo "no payfurl/platform checkout at ../platform or ../../platform: set PLATFORM_DIR" >&2; exit 1; }
    cd "$platform" && DOTNETSDK_DIR="$sdk" just e2e dotnetsdk {{args}}
