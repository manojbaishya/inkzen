#!/usr/bin/env bash

BUILD=false

# Parse the arguments
while [[ $# -gt 0 ]]; do
  case $1 in
    -b|--build )
      BUILD=true
      shift # Shift past the flag
      ;;
    * )
      echo "Unknown parameter passed: $1"
      echo "Usage: $0 [-b|--build]"
      exit 1
      ;;
  esac
done

# Check if the flag is present and run commands accordingly
if [ "$BUILD" = true ]; then
    pwsh -File RunApi.ps1 -WithBuild
else
    pwsh -File RunApi.ps1
fi

