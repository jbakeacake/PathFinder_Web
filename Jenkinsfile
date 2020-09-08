pipeline {
  agent {
    docker {
      image 'node:14.9.0-stretch-slim'
      args '-p 3000:3000'
    }

  }
  stages {
    stage('Fetch Dependencies') {
      steps {
        sh '''cd ./client-app
ls
npm install'''
      }
    }

    stage('Unit Testing') {
      steps {
        echo 'Fetch Dependencies Complete. Begin Unit Testing...'
      }
    }

    stage('Build / Compose') {
      steps {
        sh '/usr/local/bin/docker-compose build'
      }
    }

    stage('Push') {
      steps {
        sh '/usr/local/bin/docker-compose push'
      }
    }

    stage('Success') {
      steps {
        echo 'Build and Deployment Success!'
      }
    }

  }
  environment {
    PATH = '"$PATH:/usr/local/bin"'
  }
}