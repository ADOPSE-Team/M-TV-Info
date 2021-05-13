import json

def get_json(file):
    json_file = open(file)
    json_data = json.load(json_file)
    json_file.close()

    return json_data

def get_URL(config_name):
    config = get_json("config.json")
    return config.get(config_name).get("URL")

def get_gecko_location(config_name):
    config = get_json("config.json")
    return config.get(config_name).get("gecko-location")

def get_login_tests():
    tests = get_json("tests.json")
    return tests.get("login")

def get_register_tests():
    tests = get_json("tests.json")
    return tests.get("register")

def get_profile_tests():
    tests = get_json("tests.json")
    return tests.get("profile")

def get_delete_tests():
    tests = get_json("tests.json")
    return tests.get("delete")

def get_password_tests():
    tests = get_json("tests.json")
    return tests.get("password")