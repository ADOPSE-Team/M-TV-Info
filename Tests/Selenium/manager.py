import json

def get_json(file):
    json_file = open(file)
    json_data = json.load(json_file)
    json_file.close()

    return json_data

def get_URL():
    config = get_json("config.json")
    return config.get("default-config").get("URL")

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