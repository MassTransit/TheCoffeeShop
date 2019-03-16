# Loyalty Payment

Applies the customers loyalty payments to the order, which may add adjustments for lines.

## Arguments

- `Order` is the order, including lines
- `Adjustments` contains any existing adjustments, and is optional
- `LoyaltyInfo` is the customer loyalty info 

## Output

- `Adjustments` is created or updated to include any adjustments to the order based upon their loyalty status.