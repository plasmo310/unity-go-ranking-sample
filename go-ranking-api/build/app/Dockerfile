FROM golang:1.22

ENV ROOT=/usr/src/app
ENV LOCAL_DIR=./app

WORKDIR ${ROOT}

# pre-copy/cache go.mod for pre-downloading dependencies and only redownloading them in subsequent builds if they change
COPY ${LOCAL_DIR}/go.mod ${ROOT}
RUN go mod download && go mod verify

# copy local resources
COPY ${LOCAL_DIR} ${ROOT}

# make go.som
RUN go mod tidy

# build and run
CMD ["go", "run", "main.go"]
