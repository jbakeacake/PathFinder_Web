pipeline {
  agent any
  stages {
    stage('Fetch Dependencies') {
      steps {
        nodejs('Node') {
          echo 'Finished Configuring NodeJS'
          sh 'npm install'
        }

      }
    }

    stage('Unit Testing') {
      steps {
        echo 'Begin Unit Testing...'
      }
    }

    stage('Login to Docker') {
      steps {
        echo 'Logging into Docker...'
        script {
          withCredentials([usernamePassword(credentialsId: 'dockerhub_id', passwordVariable: 'DOCKER_PASS', usernameVariable: 'DOCKER_USER')])
          {
            echo env.DOCKER_USER
            echo env.DOCKER_PASS
          }
        }

      }
    }

    stage('Build / Compose') {
      steps {
        sh 'docker-compose build'
      }
    }

    stage('Push') {
      steps {
        sh 'docker-compose push'
      }
    }

    stage('Success') {
      steps {
        echo 'Build and Deployment Success!'
      }
    }

  }
}