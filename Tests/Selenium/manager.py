import json

def get_URL():
    config_file = open('config.json')
    config = json.load(config_file)
    config_file.close()

    return config.get("default-config").get("URL")

def get_login_tests():
    tests_file = open('tests.json')
    tests = json.load(tests_file)
    tests_file.close()

    return tests.get("login")

def get_register_tests():
    tests_file = open('tests.json')
    tests = json.load(tests_file)
    tests_file.close()

    return tests.get("register")
