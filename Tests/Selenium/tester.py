from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.ui import Select
from selenium.webdriver.common.keys import Keys
from pynput.keyboard import Key, Controller
from selenium import webdriver
from sys import platform
from sys import exit
from time import sleep
import manager

URL = None
driver = None

def setup():
    global URL 
    global driver 

    if (platform == "linux"):
        URL = manager.get_URL("default-config")
        driver = webdriver.Firefox()
    elif (platform == "win32"):
        URL = manager.get_URL("windows-config")
        path = manager.get_gecko_location("windows-config")
        driver = webdriver.Firefox(executable_path=path)
    else:
        print("Unsupported platform")
        exit(1)


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
            # TODO Handle more reports
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")

            print("\tUser:", tests.get(test).get("username"))
            print("\tExpected:", expected_result)
            print("\tResult:", register_result)
        
        i+=1

        close()

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

def profile(email, password, username, name, birthday, country):
    if (login(email, password)):
        driver.find_element_by_xpath("// a[contains(text(),'Hello ')]").click()

        profile_username = driver.find_element_by_id("Input_Username")
        profile_name = driver.find_element_by_id("Input_Name")
        profile_email = driver.find_element_by_id("Input_Email")
        profile_birthday = driver.find_element_by_id("Input_Birthday")
        profile_country = driver.find_element_by_id("Input_Country")

        report = [
            profile_username.get_attribute("value") == username,
            profile_name.get_attribute("value") == name,
            profile_email.get_attribute("value") == email,
            profile_birthday.get_attribute("value") == birthday,
            profile_country.get_attribute("value") == country
            ]

        return report
    else:
        return False

def execute_profile_tests():
    tests = manager.get_profile_tests()

    print("Profile Tests:", len(tests))
    i = 1

    for test in tests:
        setup()

        profile_results = profile(
            tests.get(test).get("email"),
            tests.get(test).get("password"),
            tests.get(test).get("username"),
            tests.get(test).get("name"),
            tests.get(test).get("birthday"),
            tests.get(test).get("country")
        )

        if (False):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tFailed to login")
        else:
            flag = True
            for result in profile_results:
                flag&=result
            
            if flag:
                print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
            else:
                if (profile_results[0] == False):
                    print("\tEmail doesn't match")
                if (profile_results[1] == False):
                    print("\tUsername doesn't match")
                if (profile_results[2] == False):
                    print("\tName doesn't match")
                if (profile_results[3] == False):
                    print("\tBirthday doesn't match")
                if (profile_results[4] == False):
                    print("\tCountry doesn't match")

        i+=1

        close()

def delete(email, password):
    if (login(email, password)):
        driver.find_element_by_xpath("// a[contains(text(),'Hello ')]").click()
        driver.find_element_by_xpath("// a[contains(text(),'Personal data')]").click()
        driver.find_element_by_id("delete").click()
        
        input_password = driver.find_element_by_id("Input_Password")
        input_password.send_keys(password)

        driver.find_element_by_xpath("// button[contains(text(),'Delete data and close my account')]").click()
    
        if (login(email, password)):
            return False
        else:
            return True
    else:
        return -1

def execute_delete_tests():
    tests = manager.get_delete_tests()

    print("Delete Tests:", len(tests))
    i = 1

    for test in tests:
        setup()

        delete_result = delete(
            tests.get(test).get("email"),
            tests.get(test).get("password")
        )

        if (delete_result == -1):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tFailed to login")
        elif (delete_result):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
        else:
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
            print("\tCan't delete profile")

        i+=1

        close()

def password(email, password):
    temp_password = "TempPassword1@3"

    if(login(email, password)):
        driver.find_element_by_xpath("// a[contains(text(),'Hello ')]").click()
        driver.find_element_by_xpath("// a[contains(text(),'Password')]").click()

        current_password = driver.find_element_by_id("Input_OldPassword")
        new_password = driver.find_element_by_id("Input_NewPassword")
        confirm_new_password = driver.find_element_by_id("Input_ConfirmPassword")

        current_password.send_keys(password)
        new_password.send_keys(temp_password)
        confirm_new_password.send_keys(temp_password)

        driver.find_element_by_xpath("// button[contains(text(),'Update password')]").click()
        driver.find_element_by_xpath("// button[contains(text(),'Logout')]").click()
        
        if(login(email, temp_password)):
            driver.find_element_by_xpath("// a[contains(text(),'Hello ')]").click()
            driver.find_element_by_xpath("// a[contains(text(),'Password')]").click()

            current_password = driver.find_element_by_id("Input_OldPassword")
            new_password = driver.find_element_by_id("Input_NewPassword")
            confirm_new_password = driver.find_element_by_id("Input_ConfirmPassword")

            current_password.send_keys(temp_password)
            new_password.send_keys(password)
            confirm_new_password.send_keys(password)

            driver.find_element_by_xpath("// button[contains(text(),'Update password')]").click()

            return 1
        else:
            return 0
    else:
        return -1

def execute_password_tests():
    tests = manager.get_password_tests()

    print("Password Tests:", len(tests))
    i = 1

    for test in tests:
        setup()

        password_result = password(
            tests.get(test).get("email"),
            tests.get(test).get("password")
        )

        if (password_result == 1):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
        elif (password_result == 0):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tPassword didn't change")
        else:
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tFailed to login")

        i+=1

        close()

def watchlist_add(email, password):
    if login(email, password):
        movie = driver.find_element_by_xpath("//div/div/div/div/div/div/a[1]").get_attribute("href")
        driver.get(movie)

        driver.find_element_by_id("addWatch").click()

        driver.get(URL+"Home/Watchlist")
        
        for a in driver.find_elements_by_xpath(".//a"):
            if (a.get_attribute("href") == movie):
                return 1
        return 0
    else:
        return -1

def execute_watchlist_add_tests():
    tests = manager.get_watchlist_tests()

    print("Watchlist Add tests:", len(tests))
    i = 1

    for test in tests:
        setup()

        watchlist_result = watchlist_add(
            tests.get(test).get("email"),
            tests.get(test).get("password")
        )

        if (watchlist_result == 1):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
        elif (watchlist_result == 0):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tMovie not added to watchlist")
        else:
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tFailed to login")

        i+= 1

        close()

def watchlist_remove(email, password):
    if login(email, password):
        movie = driver.find_element_by_xpath("//div/div/div/div/div/div/a[1]").get_attribute("href")
        driver.get(movie)

        driver.find_element_by_id("addWatch").click()

        driver.get(URL+"Home/Watchlist")
        
        for a in driver.find_elements_by_xpath(".//a"):
            if (a.get_attribute("href") == movie):
                return 0
        return 1
    else:
        return -1

def execute_watchlist_remove_tests():
    tests = manager.get_watchlist_tests()

    print("Watchlist Remove tests:", len(tests))
    i = 1

    for test in tests:
        setup()

        watchlist_result = watchlist_remove(
            tests.get(test).get("email"),
            tests.get(test).get("password")
        )

        if (watchlist_result == 1):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
        elif (watchlist_result == 0):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tMovie not removed from watchlist")
        else:
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tFailed to login")

        i+= 1

        close()

def favorite_add(email, password):
    if login(email, password):
        movie = driver.find_element_by_xpath("//div/div/div/div/div/div/a[1]").get_attribute("href")
        driver.get(movie)

        driver.find_element_by_id("addFav").click()

        driver.get(URL+"Home/Favorites")
        
        for a in driver.find_elements_by_xpath(".//a"):
            if (a.get_attribute("href") == movie):
                return 1
        return 0
    else:
        return -1

def execute_favorite_add_tests():
    tests = manager.get_favorite_tests()

    print("Favorite Add tests:", len(tests))
    i = 1

    for test in tests:
        setup()

        favorite_result = favorite_add(
            tests.get(test).get("email"),
            tests.get(test).get("password")
        )

        if (favorite_result == 1):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
        elif (favorite_result == 0):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tMovie not added to favorite")
        else:
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tFailed to login")

        i+= 1

        close()

def favorite_remove(email, password):
    if login(email, password):
        movie = driver.find_element_by_xpath("//div/div/div/div/div/div/a[1]").get_attribute("href")
        driver.get(movie)

        driver.find_element_by_id("addFav").click()

        driver.get(URL+"Home/Favorites")
        
        for a in driver.find_elements_by_xpath(".//a"):
            if (a.get_attribute("href") == movie):
                return 0
        return 1
    else:
        return -1

def execute_favorite_remove_tests():
    tests = manager.get_favorite_tests()

    print("Favorite Remove tests:", len(tests))
    i = 1

    for test in tests:
        setup()

        favorite_result = favorite_remove(
            tests.get(test).get("email"),
            tests.get(test).get("password")
        )

        if (favorite_result == 1):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "PASSED")
        elif (favorite_result == 0):
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tMovie not removed from favorite")
        else:
            print("[" + str(i) + "/" + str(len(tests)) + "]", "FAILED")
            print("\tFailed to login")

        i+= 1

        close()
