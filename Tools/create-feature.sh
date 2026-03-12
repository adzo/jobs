#!/bin/bash

# FastEndpoint Feature Generator
# Usage: ./create-feature.sh <FeatureName>

FEATURE_NAME=$1

if [ -z "$FEATURE_NAME" ]; then
    echo "Usage: ./create-feature.sh <FeatureName>"
    echo "Example: ./create-feature.sh GetUser"
    exit 1
fi

# Get the current directory
CURRENT_DIR=$(pwd)

# Function to find .csproj file by going up the directory tree
find_csproj() {
    local dir="$1"
    while [ "$dir" != "/" ]; do
        local csproj_file=$(find "$dir" -maxdepth 1 -name "*.csproj" -print -quit)
        if [ -n "$csproj_file" ]; then
            echo "$csproj_file"
            return 0
        fi
        dir=$(dirname "$dir")
    done
    return 1
}

# Find the .csproj file
CSPROJ_PATH=$(find_csproj "$CURRENT_DIR")

if [ -z "$CSPROJ_PATH" ]; then
    echo "❌ Error: Could not find .csproj file in parent directories"
    exit 1
fi

# Get the project name from .csproj filename
PROJECT_NAME=$(basename "$CSPROJ_PATH" .csproj)
CSPROJ_DIR=$(dirname "$CSPROJ_PATH")

# Calculate the relative path from .csproj directory to current directory
RELATIVE_PATH=$(realpath --relative-to="$CSPROJ_DIR" "$CURRENT_DIR" 2>/dev/null)

# For macOS, use python if realpath doesn't work
if [ $? -ne 0 ]; then
    RELATIVE_PATH=$(python3 -c "import os.path; print(os.path.relpath('$CURRENT_DIR', '$CSPROJ_DIR'))")
fi

# Build the namespace
if [ "$RELATIVE_PATH" = "." ]; then
    # We're in the same directory as .csproj
    NAMESPACE="$PROJECT_NAME"
else
    # Convert path separators to dots
    NAMESPACE_SUFFIX=$(echo "$RELATIVE_PATH" | tr '/' '.')
    NAMESPACE="${PROJECT_NAME}.${NAMESPACE_SUFFIX}"
fi

echo "📦 Project: $PROJECT_NAME"
echo "📂 Namespace: $NAMESPACE"
echo "🚀 Creating feature: $FEATURE_NAME"
echo ""

FEATURE_LOWER=$(echo "$FEATURE_NAME" | tr '[:upper:]' '[:lower:]')

# Feature.cs
cat > "${FEATURE_NAME}.cs" << EOF
using FastEndpoints;

namespace ${NAMESPACE};

internal sealed class ${FEATURE_NAME} : Endpoint<${FEATURE_NAME}Request, ${FEATURE_NAME}Response>
{
    public override void Configure()
    {
        throw new NotImplementedException();
    }

    public override async Task HandleAsync(${FEATURE_NAME}Request req, CancellationToken ct)
    {
        await Task.Delay(50);
        throw new NotImplementedException();
    }
}
EOF

# Request.cs
cat > "${FEATURE_NAME}.Request.cs" << EOF
namespace ${NAMESPACE};

internal sealed record ${FEATURE_NAME}Request
{
    
}
EOF

# RequestValidator.cs
cat > "${FEATURE_NAME}.RequestValidator.cs" << EOF
using FastEndpoints;
using FluentValidation;

namespace ${NAMESPACE};

internal sealed class ${FEATURE_NAME}RequestValidator : Validator<${FEATURE_NAME}Request>
{
    public ${FEATURE_NAME}RequestValidator()
    {
        // Add validation rules here
    }
}
EOF

# Response.cs
cat > "${FEATURE_NAME}.Response.cs" << EOF
namespace ${NAMESPACE};

internal sealed record ${FEATURE_NAME}Response
{
    
}
EOF

echo "✅ Created files:"
echo "   📄 ${FEATURE_NAME}.cs"
echo "   📄 ${FEATURE_NAME}.Request.cs"
echo "   📄 ${FEATURE_NAME}.RequestValidator.cs"
echo "   📄 ${FEATURE_NAME}.Response.cs"
echo ""
echo "✨ Done!"