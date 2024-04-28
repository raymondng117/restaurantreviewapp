# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Install ngrok
RUN curl -s https://ngrok-agent.s3.amazonaws.com/ngrok.asc \
    | tee /etc/apt/trusted.gpg.d/ngrok.asc >/dev/null && \
    echo "deb https://ngrok-agent.s3.amazonaws.com buster main" \
    | tee /etc/apt/sources.list.d/ngrok.list && \
    apt-get update && \
    apt-get install -y ngrok

# Set the working directory inside the container
WORKDIR /app

# Copy the .NET app project files to the container
COPY . .

# Restore dependencies
RUN dotnet restore

# Build the app
RUN dotnet publish -c Release -o out

# Build the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Install ngrok in the final image
COPY --from=build-env /usr/local/bin/ngrok /usr/local/bin/ngrok

# Set the working directory inside the container
WORKDIR /app

# Copy the published app from build-env to the final image
COPY --from=build-env /app/out .

# Copy the Data folder containing your XML file
COPY Data /app/Data

# Copy the start script into the container
COPY start.sh .

# Provide execute permissions to the start script
RUN chmod +x start.sh

# Specify the command to run the app when the container starts
ENTRYPOINT ["./start.sh"]
