pipeline {
  agent {
    docker {
      image 'node:10.14.0-alpine'
    }

  }
  stages {
    stage('Fetch Dependencies') {
      steps {
        sh '''npm install -g @angular/cli
ls'''
      }
    }

    stage('Unit Testing') {
      steps {
        echo 'Fetch Dependencies Complete. Begin Unit Testing...'
      }
    }

  }
}