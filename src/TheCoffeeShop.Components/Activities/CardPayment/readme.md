# Card Payment

Handles credit card payments using the security payment vault. If the transaction is compensated, the authorization is cleared and the charge reversed. 
Authorizations which are authorized and not compensated within two hours are completed. The amount can be adjusted after authorization to account for a tip 
addition.

## Arguments

- `PaymentDue` is used to charge the card. If missing or a zero balance, no card transaction is performed.

## Output

- `PaymentDue` is updated to reflect the remaining amount after the credit card payment is applied.