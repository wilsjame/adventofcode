"""
(push w1 + 12)
(push w2 + 9)
(push w3 + 8)
pop-8=w4
(push w5 + 0)
(push w6 + 11)
(push w7 + 10)
pop-11=w8
(push w9 + 3)
pop-1=w10
pop-8=w11
pop-5=w12
pop-16=w13
pop-6=w14

(w3 + 8) - 8 = w4
(w7 + 10) - 11 = w8
(w9 + 3) – 1 = w10
(w6 + 11) – 8 = w11
(w5 + 0) – 5 = w12
(w2 + 9) – 16 = w13
(w1 + 12) – 6 = w14

w1 = w14 - 6
w2 = w13 + 7
w3 = w4
w5 = w12 + 5
w6 = w11 - 3
w7 = w8 + 1
w9 = w10 - 2 

# Part One: maximize by picking highest numbers for w1, w2,...

w1 = 3, w14 = 9
w2 = 9, w13 = 2
w3 = 9, w4 = 9
w5 = 9, w12 = 4
w6 = 6, w11 = 9
w7 = 9, w8 = 8
w9 = 7, w10 = 9

39999698799429

# Part Two: minimize by picking lowest numbers for w1, w2,...

w1 = 1, w14 = 7
w2 = 8, w13 = 1
w3 = 1, w4 = 1
w5 = 6, w12 = 1
w6 = 1, w11 = 4
w7 = 2, w8 = 1
w9 = 1, w10 = 3

18116121134117

"""
