import numpy as np
import matplotlib.pyplot as plt
import pandas as pd

data1 = {
    'Convergence x1 ||xk - xk+1||':
[
4.86985043447,
5.41277157813,
2.03323104455,
0.27147143661,
0.01326318890,
0.00004157625,
0.00000000037
                   ],
        'Convergence x2 ||xk - xk+1||':
        [
9.16913559881,
0.44375312296,
0.93334999540,
0.41482305051,
0.02365541469,
0.00007005715,
0.00000000064,
        ],
    'Convergence x3 ||xk - xk+1||':
        [
14.03898603321,
5.856524701090,
1.099881049150,
0.143351613890,
0.010392225800,
0.000028480900,
0.000000000270
        ],
}

# Use dictionary: (data1, data2, data3, data4, data5 ... data8) to get graphics of every root

df = pd.DataFrame(data1)

# 1 root, x = np.arange(39)
# 2 root, x = np.arange(39)
# 3 root, x = np.arange(38)
# 4 root, x = np.arange(40)
# 5 root, x = np.arange(40)
# 6 root, x = np.arange(41)
# 7 root, x = np.arange(41)
# 8 root, x = np.arange(41)
x = np.arange(7)
plt.semilogy(base=10)
plt.plot(x, df)
# Use the same data, that you used before in function "DataFrame"
plt.legend(data1, loc=2)
plt.show()