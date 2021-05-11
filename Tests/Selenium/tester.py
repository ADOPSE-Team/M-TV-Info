from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import Select
from pynput.keyboard import Key, Controller
from selenium import webdriver
from time import sleep
import manager

URL = None
driver = None

def setup():
    global URL 
    URL = manager.get_URL()
    global driver 
    driver = webdriver.Firefox()

def close():
    driver.close()    

def register(username, name, email, password, confirm_password, birthday, country):
    driver.get(URL)

    register_button = driver.find_element_by_xpath("// a[contains(text(),'Register')]")
    register_button.click()

    input_username = driver.find_element_by_id("Input_Username")
    input_name = driver.find_element_by_id("Input_Name")
    input_email = driver.find_element_by_id("Input_Email")
    input_password = driver.find_element_by_id("Input_Password")
    input_confirm_password = driver.find_element_by_id("Input_ConfirmPassword")
    input_birthday = driver.find_element_by_id("Input_Birthday")
    input_country = driver.find_element_by_id("Input_Country")

    input_username.send_keys(username)
    input_name.send_keys(name)
    input_email.send_keys(email)
    input_password.send_keys(password)
    input_confirm_password.send_keys(confirm_password)
    input_birthday.click()
    input_confirm_password.click()

    # TODO Must be changed, problematic
    input_birthday.click()
    input_birthday.send_keys("")
    keyboard = Controller()
    for num in birthday:
        sleep(.1)
        keyboard.press(num)

    select = Select(input_country)
    select.select_by_visible_text(country)

    driver.find_element_by_xpath("// button[contains(text(),'Register')]").click()
    try:
        driver.find_element_by_id("confirm-link").click()
        return True
    except:
        return driver.find_element_by_class_name("validation-summary-errors").text

def login(username, password):
    driver.get(URL)

    driver.find_element_by_xpath("// a[contains(text(),'Login')]").click()

    email_field = driver.find_element_by_id("Input_Email")
    password_field = driver.find_element_by_id("Input_Password")

    email_field.send_keys(username)
    password_field.send_keys(password)

    password_field.send_keys(Keys.RETURN)
    
    try:
        sleep(2)
        driver.find_element_by_id("Input_Email")
        return False
    except:
        return True
    
def execute_register_tests():
    tests = manager.get_register_tests()

    print("Register Tests:", len(tests))
    i = 1
    
    for test in tests:
        setup()

        expected_result = tests.get(test).get("result") == "True"
        register_result = register(
            tests.get(test).get("username"),
            tests.get(test).get("name"),
            tests.get(test).get("email"),
            tests.get(test).get("password"),
            tests.get(test).get("confirm_password"),
            tests.get(test).get("birthday"),
            tests.get(test).get("country")
        )

        if (expected_result == register_result):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
        else:
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")

            print("\tUser:", tests.get(test).get("username"))
            print("\tExpected:", expected_result)
            print("\tResult:", register_result)
        
        i+=1

        close()

def execute_login_tests():
    tests = manager.get_login_tests()

    print("Login Tests:", len(tests))
    i = 1

    for test in tests:
        setup()

        expected_result = tests.get(test).get("result") == "True"
        login_results = login(
            tests.get(test).get("username"),
            tests.get(test).get("password")
            )

        if (expected_result == login_results):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
        else:
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")

            print("\tUser:", tests.get(test).get("username"))
            print("\tExpected:", expected_result)
            print("\tResult:", login_results)

        i+=1

        close()
