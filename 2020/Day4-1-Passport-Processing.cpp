#include <iostream>
#include <string>
#include <algorithm>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string S, batch;

    int ans = 0;
    while (getline(cin, S)) {
        if (S.length() > 0) {
            batch += S;
        }
        if (S.length() == 0 || cin.peek() == EOF) {
            if (batch.find("byr") != std::string::npos &&
                batch.find("iyr") != std::string::npos &&
                batch.find("eyr") != std::string::npos &&
                batch.find("hgt") != std::string::npos &&
                batch.find("hcl") != std::string::npos &&
                batch.find("ecl") != std::string::npos &&
                batch.find("pid") != std::string::npos) 
            {
                ans++;
            }
            batch = "";
        }
    }
    cout << ans << endl;

    return 0;
}
