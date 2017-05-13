FROM ubuntu:latest

# Install mono packages
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
RUN echo "deb http://download.mono-project.com/repo/debian wheezy main" | tee /etc/apt/sources.list.d/mono-xamarin.list
RUN apt-get update

RUN apt-get install -y mono-devel
RUN apt-get install -y mono-complete
RUN apt-get install -y referenceassemblies-pcl
RUN apt-get install -y ca-certificates-mono
RUN apt-get install -y expect

# Create app directory
RUN mkdir -p /usr/src/app
ADD . /usr/src/app
WORKDIR /usr/src/app

